using Join;
using Join.Dal;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Linq;

namespace TransactionAppletaApi
{
    [RoutePrefix("api/_pay")]
    //[Security.AuthorizationRequired]
    public class PayController : BaseController
    {
        #region 微信服务商支付接口
        /// <summary>
        /// 微信服务商支付接口
        /// </summary>
        [HttpPost]
        [Route("WeChatServicesPayApi")]
        public IHttpActionResult WeChatServicesPayApi([FromBody]JToken json)
        {
            return this.TryReturn<object>(() =>
            {
                try
                {
                    WxPayData wxp = new WxPayData();
                    wxp.WriteLogFile("调用支付Json:" + json.ToJsonString());
                    var arg = json.AsDynamic();
                    var ip = GetClientIpAddress();
                    //金额
                    string price = arg.price;
                    //订单号
                    string orderNo = arg.orderNo;
                    //产品ID
                    string productId = arg.productId;
                    //买家ID
                    string buyUserId = arg.buyUserId;
                    //JsCode
                    string jsCode = arg.jsCode;
                    //获取OpenId
                    var openId = WxPayData.GetOpenId(jsCode).openid;
                    if (openId == "" || openId == null)
                    {
                        var msg = "JSCODE " + jsCode + "获取不到openId";
                        wxp.WriteLogFile(msg);
                        return new { Table = new { MSG = "", IsSuccess = false, ErroMessage = msg } };
                    }
                    #region 锁定产品状态
                    //执行sql
                    using (var x = Join.Dal.MySqlProvider.X())
                    {
                        //获取产品ID 查询产品是否在上架时间并且状态为上架中
                        var nowDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var selectProductSql = "select * from B_PRODUCT_LIST where status='上架中' and kid='" + productId + "' and OFF_SHELF_TIME > '" + nowDate + "'";
                        var selectProductTables = x.ExecuteSqlCommand(selectProductSql);
                        if (selectProductTables.Tables[0].Rows.Count > 0)
                        {
                            //执行调用付款
                            //构造附加数据
                            string attach = productId + "|" + buyUserId;
                            var url = GlobalVariableWeChatApplets.UNIFIEDORDER_URL;
                            var data = WxPayData.ForApplets(double.Parse(price), openId, orderNo, ip, attach);
                            var xml = data.ToXml();
                            var response = HttpService.Post(xml, url, 6);
                            var preOrder = WxPayData.FromXml(response, "HMAC-SHA256");
                            var errCode = preOrder.GetValue("err_code");
                            if (errCode != null)
                            {
                                var errMsg = preOrder.GetValue("err_code_des");
                                return new { Table = new { MSG = "", IsSuccess = false, ErroMessage = errMsg } };
                            }
                            else
                            {
                                //如果调起支付成功，锁定产品状态为已锁定
                                var updateProductSql = "update b_product_list set status='已锁定',LOCK_TIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where kid = '" + productId + "'";
                                x.ExecuteSqlCommand(updateProductSql);

                                var payData = WxPayData.ForWechatPay(preOrder);
                                var orderString = payData.ToJson();
                                return new { Table = new { MSG = orderString, IsSuccess = true, ErroMessage = string.Empty } };
                            }
                        }
                        else
                        {
                            return new { Table = new { MSG = "", IsSuccess = false, ErroMessage = "产品已下架。" } };
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    return new { Table = new { MSG = "", IsSuccess = false, ErroMessage = ex.Message } };
                }
            });
        }

        #endregion

        public class searchModel
        {
            public string trade_state { get; set; }
            public string trade_state_desc { get; set; }
        }

        #region 微信服务商退款接口
        /// <summary>
        /// 微信服务商退款接口
        /// </summary>
        [HttpPost]
        [Route("WeChatServicesRefundApi")]
        public IHttpActionResult WeChatServicesRefundApi([FromBody]JToken json)
        {
            return this.TryReturn<object>(() =>
            {
                try
                {
                    WxPayData wxp = new WxPayData();
                    //插入调用退款接口Json
                    wxp.WriteLogFile("调用退款Json:" + json.ToJsonString());
                    var arg = json.AsDynamic();
                    //订单号
                    string orderNo = arg.orderNo;
                    //查询要退款的单据
                    var selectOrderSql = string.Format(@"SELECT * FROM B_ORDER WHERE CODE='{0}' AND PAY_STATUS='已支付' AND IS_DELETE=0", orderNo, this.LoginUser.Cid);
                    wxp.WriteLogFile("执行单据退款查询SQL：" + selectOrderSql);
                    var recordList = new DataSet();
                    using (var x = MySqlProvider.X())
                    {
                        recordList = x.ExecuteSqlCommand(selectOrderSql);
                        x.Close();
                    }
                    var recordRows = recordList.Tables[0].DataSet.Rows().FirstOrDefault();
                    if (recordRows != null)
                    {
                        var item = recordRows;
                        var refundNo = "RFD" + orderNo;
                        //订单金额
                        string price = item["PRICE"].ToString();

                        var data = WxPayData.ForRefund(double.Parse(price), double.Parse(price), orderNo, refundNo);
                        var url = GlobalVariableWeChatApplets.REFUND_URL;
                        var xml = data.ToXml();
                        var response = HttpService.PostByCertificates(xml, url, 6);
                        var preOrder = WxPayData.FromXml(response, "HMAC-SHA256");
                        var backCode = preOrder.GetValue("return_code");
                        if (backCode != null && backCode.ToString() == "FAIL")
                        {
                            var returnMsg = preOrder.GetValue("return_msg").ToString();
                            return new { Table = new { IsSuccess = false, ErroMessage = returnMsg, MSG = "" } };
                        }
                        else
                        {
                            var payData = WxPayData.ForWechatRefund(preOrder);
                            var orderString = payData.ToJson();
                            return new { Table = new { IsSuccess = true, ErroMessage = "", MSG = orderString } };
                        }
                    }
                    else
                    {
                        return new { Table = new { IsSuccess = false, ErroMessage = "未查询到需要退款的数据", MSG = "" } };
                    }
                }
                catch (Exception ex)
                {
                    return new { Table = new { IsSuccess = false, ErroMessage = ex.Message, Json = "" } };
                }
            });
        }
        #endregion

        #region GetIP
        private const string HttpContextt = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string IPRegex = @"^([1-9]|([1-9]\d)|(1\d\d)|(2([0-4]\d|5[0-5])))\.(([0-9]|([1-9]\d)|(1\d\d)|(2([0-4]\d|5[0-5])))\.){2}([1-9]|([1-9]\d)|(1\d\d)|(2([0-4]\d|5[0-5])))$";
        private string GetClientIpAddress()
        {
            var result = string.Empty;
            if (Request.Properties.ContainsKey(HttpContextt))
            {
                dynamic ctx = Request.Properties[HttpContextt];
                if (ctx != null)
                {
                    result = ctx.Request.UserHostAddress;
                }
            }
            if (Request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = Request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    result = remoteEndpoint.Address;
                }
            }
            if (Regex.IsMatch(result, IPRegex))
                return result;
            return "127.0.0.1";
        }
        #endregion
    }
}
