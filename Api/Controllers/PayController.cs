using Join;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;
using System.Web.Http;

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
        /// <param name="json">包含价格订单号描述OpenId子商户号的Json对象</param>
        /// <returns>支付结果</returns>
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

                    ////前缀，避免测试服务器与正式服务器订单号重复
                    //var prefix = System.Configuration.ConfigurationManager.AppSettings["TransactionPrefix"];
                    var arg = json.AsDynamic();
                    var ip = GetClientIpAddress();
                    //金额
                    string price = arg.price;
                    //是否分账
                    string profit_sharing = arg.profit_sharing;
                    //订单号
                    string jNo = arg.orderNo;
                    string orderNo = jNo + "_" + this.LoginUser.Cid + "_" + profit_sharing;
                    //string orderNo = prefix + jNo + "_" + this.LoginUser.Cid;
                    //描述
                    string description = arg.description;
                    //子商户号
                    string subMchId = arg.subMchId;
                    //JsCode
                    string jsCode = arg.jsCode;
                    //调起支付的小程序ID
                    string subAppId = arg.subAppId;
                    //ApiUrl
                    string apiUrl = arg.apiUrl;
                    //存储过程名称
                    string spName = arg.spName;
                    var attach = "{'spName':'" + spName + "','apiUrl':'" + apiUrl + "'}";
                    //获取OpenId
                    var openId = WxPayData.GetOpenId(jsCode).openid;
                    if (openId == "")
                    {
                        var msg = "JSCODE " + jsCode + "获取不到openId";
                        wxp.WriteLogFile(msg);
                        return new { Table = new { MSG = "", IsSuccess = false, ErroMessage = msg } };
                    }
                    var url = GlobalVariableWeChatApplets.UNIFIEDORDER_URL;
                    var data = WxPayData.ForApplets(double.Parse(price), orderNo, description, ip);
                    var xml = data.ToXml();
                    var response = HttpService.Post(xml, url, 6);
                    var preOrder = WxPayData.FromXml(response);
                    var errCode = preOrder.GetValue("err_code");
                    if (errCode != null)
                    {
                        var errMsg = preOrder.GetValue("err_code_des");
                        return new { Table = new { MSG = "", IsSuccess = false, ErroMessage = errMsg } };
                    }
                    else
                    {
                        var payData = WxPayData.ForWechatPay(preOrder);
                        var orderString = payData.ToJson();
                        return new { Table = new { MSG = orderString, IsSuccess = true, ErroMessage = string.Empty } };
                    }
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

        //#region 微信服务商退款接口
        ///// <summary>
        ///// 微信服务商退款接口
        ///// </summary>
        ///// <param name="json"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[Route("WeChatServicesRefundApi")]
        //public IHttpActionResult WeChatServicesRefundApi([FromBody]JToken json)
        //{
        //    return this.TryReturn<object>(() =>
        //    {
        //        try
        //        {
        //            var baseUrl = System.Configuration.ConfigurationManager.AppSettings["wechatProxy_url"];
        //            WxPayData wxp = new WxPayData();
        //            ////前缀，避免测试服务器与正式服务器订单号重复
        //            //var prefix = System.Configuration.ConfigurationManager.AppSettings["TransactionPrefix"];
        //            //插入调用退款接口Json
        //            wxp.WriteLogFile("调用退款Json:" + json.ToJsonString());
        //            var arg = json.AsDynamic();
        //            //是否分账
        //            string profit_sharing = arg.profit_sharing;
        //            //订单号
        //            string orderNo = arg.orderNo;
        //            string wecharOrderNo = orderNo + "_" + this.LoginUser.Cid;
        //            if (profit_sharing == "Y")
        //                wecharOrderNo += "_Y";
        //            else
        //                wecharOrderNo += "_N";
        //            //string wecharOrderNo = prefix + orderNo + "_" + this.LoginUser.Cid;
        //            //子商户号
        //            string subMchId = arg.subMchId;
        //            //ApiUrl
        //            string apiUrl = arg.apiUrl;
        //            //调起支付的小程序ID
        //            string subAppId = arg.subAppId;
        //            //退款单品ID
        //            string productId = arg.productId;
        //            //退款金额
        //            string productAmount = arg.productAmount;

        //            #region 查询订单状态（微信方）是否支持退款
        //            var serachOrderStatusUrl = baseUrl + "api-mch/pay/searchOrder/" + GlobalVariableWeChatApplets.MCH_ID;
        //            var searchParmar = new
        //            {
        //                appid = GlobalVariableWeChatApplets.APPID,
        //                mch_id = GlobalVariableWeChatApplets.MCH_ID,
        //                sub_appid = subAppId,
        //                sub_mch_id = subMchId,
        //                out_trade_no = wecharOrderNo
        //            };
        //            var result = WxPayData.GetDataByUrl(serachOrderStatusUrl, searchParmar.ToJsonString());
        //            bool isSucess = result.Table.IsSuccess;
        //            if (isSucess == false)
        //            {
        //                wxp.WriteLogFile("查询微信订单状态失败,失败原因：" + result.Table.ErroMessage);
        //                return result;
        //            }
        //            else
        //            {
        //                var dataJson = result.Table.MSG;
        //                var searchModel = JsonConvert.DeserializeObject<searchModel>(dataJson);
        //                string payStatus = searchModel.trade_state;
        //                if (payStatus != "SUCCESS")
        //                {
        //                    return result;
        //                }
        //            }
        //            #endregion

        //            #region 调用退款存储过程(账户退款)
        //            using (var x = Join.Dal.MySqlProvider.X())
        //            {
        //                var jsonBack = new { code = orderNo, cid = this.LoginUser.Cid, crtCode = this.LoginUser.Code };
        //                var mySqlResult = x
        //                    .ParamIn("p", MySqlDbType.JSON, jsonBack.ToJsonString())
        //                    .ExecuteStoredProcedure("create_return_retail_account_record");
        //                x.Close();
        //                var table = mySqlResult.FirstTable();
        //                var errorCode = table.Rows[0]["ERROR_CODE"].ToString();
        //                if (errorCode != "0")
        //                {
        //                    var errorMsg = table.Rows[0]["MSG"].ToString();
        //                    return new { Table = new { IsSuccess = false, ErroMessage = errorMsg, MSG = "" } };
        //                }
        //            }
        //            #endregion

        //            #region 查询分账明细表是否存在未分账的数据，如果存在，执行分账回退。否则，执行单据退款
        //            if (profit_sharing.ToUpper() == "Y")
        //            {
        //                //查询分账明细表已分账数据
        //                var sql = string.Format(@"SELECT A.KID,A.ACCOUNT,A.AMOUNT,B.SUBMERCHANT_ID,B.OUT_ORDER_NO FROM b_retail_sales_order_xprofitsharingdetail 
        //                                    AS A LEFT JOIN b_retail_sales_order AS B 
        //                                    ON A.PID = B.`KID` WHERE B.PAY_CODE ='{0}' AND B.CID='{1}' AND A.IS_BACK=0 ", orderNo, this.LoginUser.Cid);
        //                wxp.WriteLogFile("执行分账回退查询Sql：" + sql);
        //                using (var x = Join.Dal.MySqlProvider.X())
        //                {
        //                    //返回值
        //                    var isMainReturn = true;
        //                    var msg = "";

        //                    var dt = x.ExecuteSqlCommand(sql);
        //                    var rows = dt.Tables[0].Rows;
        //                    //如果存在待退款的明细
        //                    if (dt.Tables[0].Rows.Count > 0)
        //                    {
        //                        //循环待分账回退列表
        //                        foreach (var item in rows)
        //                        {
        //                            var dataKid = dt.Tables[0].Rows[0]["KID"].ToString();
        //                            var profit_subMchId = dt.Tables[0].Rows[0]["SUBMERCHANT_ID"].ToString();
        //                            var out_order_no = dt.Tables[0].Rows[0]["OUT_ORDER_NO"].ToString();
        //                            var return_account = dt.Tables[0].Rows[0]["ACCOUNT"].ToString();
        //                            var return_amount = dt.Tables[0].Rows[0]["AMOUNT"].ToString();
        //                            //商户回退单号 自己生成
        //                            var timeStr = DateTime.Now.ToString("yyyyMMddHHmmss");
        //                            string out_return_no = "htd" + timeStr;

        //                            //商户分帐单号 零售表查交易号
        //                            //商户回退单号 自己生成
        //                            //var url = GlobalVariableWeChatApplets.PROFITSHARING_RETURN_URL;
        //                            var url = baseUrl + "api-mch/pay/profitsharingRefound/" + GlobalVariableWeChatApplets.MCH_ID;
        //                            var data = WxPayData.ForProfitsharingReturnApiApplets(subMchId, out_order_no, out_return_no, return_account, return_amount, "用户退款");
        //                            var strJson = data.ToJson();
        //                            var profitshResult = WxPayData.GetDataByUrl(url, strJson);
        //                            if (profitshResult.Table.IsSuccess)
        //                            {
        //                                //如果分账回退成功，执行sql，修改零售订单明细中的数据回退状态为true
        //                                var updateSql = string.Format(@"UPDATE b_retail_sales_order_xprofitsharingdetail SET IS_BACK=1 WHERE KID='{0}'", dataKid);
        //                                x.ExecuteSqlCommand(updateSql);
        //                            }
        //                            if (isMainReturn)
        //                            {
        //                                isMainReturn = profitshResult.Table.IsSuccess;
        //                                if (profitshResult.Table.IsSuccess == false)
        //                                    msg += "账号：" + return_account + "执行分账退款失败，失败原因：" + profitshResult.Table.ErroMessage + "/";
        //                            }
        //                            //分账回退结果写入日志
        //                            wxp.WriteLogFile("执行分账回退结果：" + profitshResult.Table.ToJsonString());
        //                        }
        //                        //如果明细没有全部退款
        //                        if (isMainReturn == false)
        //                            return new { Table = new { IsSuccess = false, ErroMessage = msg, MSG = "分账明细未全部回退" } };
        //                    }
        //                }
        //            }
        //            #endregion

        //            #region 执行正常单据退款
        //            //获取要退款的数据
        //            //2020年6月10日11:28:54增加关联零售订单表 用PAY_CODE过滤数据
        //            var sql1 = string.Format(@"SELECT B.PAY_CODE,A.* FROM b_retail_account_record AS A LEFT JOIN b_retail_sales_order AS B
        //                                ON A.ASSOCIATE_ORDER_ID=B.KID WHERE PAY_CODE='{0}' AND A.CID='{1}' AND A.IS_DELETE=0 
        //                                AND A.TYPE='Consume' AND A.PAY_METHOD='WechatPay'", orderNo, this.LoginUser.Cid);
        //            wxp.WriteLogFile("执行正常单据退款查询SQL：" + sql1);
        //            var recordList = new DataSet();
        //            using (var x = MySqlProvider.X())
        //            {
        //                recordList = x.ExecuteSqlCommand(sql1);
        //                x.Close();
        //            }
        //            var recordRows = recordList.Tables[0].DataSet.Rows().FirstOrDefault();
        //            if (recordRows != null)
        //            {
        //                var item = recordRows;
        //                var refundNo = "";
        //                //退款单号 拼接CID CRT_CODE
        //                if (productId != null && productId != "")
        //                    //refundNo = orderNo + "_" + 34 + "_" + "sa" + "_" + productId;
        //                    refundNo = orderNo + "_" + this.LoginUser.Cid.ToString() + "_" + this.LoginUser.Code + "_" + productId;
        //                else
        //                    //refundNo = orderNo + "_" + 34 + "_" + "sa" ;
        //                    refundNo = orderNo + "_" + this.LoginUser.Cid.ToString() + "_" + this.LoginUser.Code;

        //                //订单金额
        //                string price = item["AMOUNT"].ToString();
        //                //退款金额
        //                string refundPrice = "";
        //                if (productAmount != null && productAmount != "")
        //                    refundPrice = productAmount;
        //                else
        //                    refundPrice = item["AMOUNT"].ToString();

        //                var data = WxPayData.ForRefund(double.Parse(price), double.Parse(refundPrice), wecharOrderNo, refundNo, subMchId, apiUrl, subAppId);
        //                var refundUrl = baseUrl + "api-mch/pay/refund/" + GlobalVariableWeChatApplets.MCH_ID;
        //                var strJson = data.ToJson();
        //                var refundResult = WxPayData.GetDataByUrl(refundUrl, strJson);
        //                if (refundResult.Table.IsSuccess)
        //                {
        //                    using (var x = Join.Dal.MySqlProvider.X())
        //                    {
        //                        //如果申请退款成功，调用存储过程
        //                        var jsonBack = new { cid = this.LoginUser.Cid, orderNo = orderNo, crtCode = this.LoginUser.Code, productId = productId ?? "" };
        //                        var mySqlResult = x
        //                            .ParamIn("p", MySqlDbType.JSON, jsonBack.ToJsonString())
        //                            .ExecuteStoredProcedure("update_wechat_return_retail_order");
        //                        x.Close();
        //                        var table = mySqlResult.FirstTable();
        //                        var errorCode = table.Rows[0]["ERROR_CODE"].ToString();
        //                        if (errorCode != "0")
        //                        {
        //                            var errorMsg = table.Rows[0]["MSG"].ToString();
        //                            return new { Table = new { IsSuccess = false, ErroMessage = errorMsg, MSG = "" } };
        //                        }
        //                        return refundResult;
        //                    }
        //                }
        //                else
        //                {
        //                    return refundResult;
        //                }
        //            }
        //            else
        //            {
        //                return new { Table = new { IsSuccess = false, ErroMessage = "未查询到需要退款的数据", MSG = "" } };
        //            }
        //            #endregion
        //        }
        //        catch (Exception ex)
        //        {
        //            return new { Table = new { IsSuccess = false, ErroMessage = ex.Message, Json = "" } };
        //        }
        //    });
        //}
        //#endregion

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
