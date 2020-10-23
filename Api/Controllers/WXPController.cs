using Join;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Http;

namespace TransactionAppletaApi
{
    [RoutePrefix("api/_wxp")]
    public class WXPController : BaseController
    {
        #region 回调Api

        #region 支付回调
        [Route("tenpay_notify")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Tenpay()
        {
            //生成单据
            var result = Request.Content.ReadAsStringAsync().Result;
            this.WriteLogFile("回调参数:" + result);
            var bizJson = WxPayData.ParaseNotify(result);
            var bizObj = JsonConvert.DeserializeObject<WxPayData.NOTIFY>(bizJson);
            var orderNo = bizObj.SALES_NO;
            var tranId = bizObj.TRANSACTION_ID;
            //var tranId = bizObj.TRANSACTION_ID;
            var arrachArray = bizObj.ARRACH.Split('|');
            var productId = arrachArray[0];
            var buyUserId = arrachArray[1];
            var response = new HttpResponseMessage();

            Cache c = HttpRuntime.Cache;
            var isExit = c.Get(productId);
            if (isExit != null)
            {
                this.WriteLogFile("已收到回调");
                response.Content = new StringContent(WxPayData.NotifySuccess());
                response.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
            }
            else
            {
                //插入缓存，标识已收到微信回调
                c.Insert(productId, bizObj.ARRACH);
                //执行生成订单逻辑
                var isCreate = CreateOrder(productId, buyUserId, orderNo, tranId);
                if (isCreate)
                {
                    WriteLogFile("TenpayOK!");
                    response.Content = new StringContent(WxPayData.NotifySuccess());
                    response.Content.Headers.ContentType
                    = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
                }
                else
                {
                    WriteLogFile("TenpaySysEx:" + "单据生成错误");
                    response.Content = new StringContent(WxPayData.NotifyFail());
                    response.Content.Headers.ContentType
                      = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
                }
            }
            return response;
        }
        #endregion

        #region 退款回调
        [Route("refundApi")]
        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage RefundApi()
        {
            var response = new HttpResponseMessage();
            try
            {
                var result = Request.Content.ReadAsStringAsync().Result;
                WriteLogFile("回调参数:" + result);
                var bizJson = WxPayData.RefundJson(result);
                var bizObj = JsonConvert.DeserializeObject<WxPayData.REFUND_NOTIFY>(bizJson);
                var status = bizObj.refund_status;
                var out_refund_no = bizObj.out_refund_no;
                var orderNo = bizObj.out_trade_no;
                if (status == "SUCCESS")
                {
                    Cache c = HttpRuntime.Cache;
                    var isExit = c.Get(out_refund_no);
                    if (isExit != null)
                    {
                        this.WriteLogFile("已收到回调");
                        response.Content = new StringContent(WxPayData.NotifySuccess());
                        response.Content.Headers.ContentType
                        = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
                    }
                    else
                    {
                        //插入缓存，标识已收到微信回调
                        c.Insert(out_refund_no, orderNo);
                        //执行单据退款逻辑
                        var isRefund = this.Refund(orderNo);
                        if (isRefund)
                        {
                            WriteLogFile("TenpayOK!");
                            response.Content = new StringContent(WxPayData.NotifySuccess());
                            response.Content.Headers.ContentType
                            = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
                        }
                        else
                        {
                            WriteLogFile("TenpaySysEx:" + "单据退款失败");
                            response.Content = new StringContent(WxPayData.NotifyFail());
                            response.Content.Headers.ContentType
                              = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
                        }
                    }
                }
                else
                {
                    WriteLogFile("TenpayEx:" + "回调失败");
                    response.Content = new StringContent(WxPayData.NotifyFail());
                    response.Content.Headers.ContentType
              = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
                    return response;
                }
            }
            catch (Exception ex)
            {
                WriteLogFile("TenpayEx:" + ex.Message);
                response.Content = new StringContent(WxPayData.NotifyFail());
                response.Content.Headers.ContentType
                  = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
                return response;
            }
            WriteLogFile("TenpayOK!");
            response.Content = new StringContent(WxPayData.NotifySuccess());
            response.Content.Headers.ContentType
                    = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
            return response;
        }
        #endregion

        #endregion

        #region WriteLogFile
        public void WriteLogFile(string input)
        {
            DateTime now = DateTime.Now;
            var date = now.Year + "" + now.Month + "" + now.Day;
            var time = now.ToLongTimeString();
            var application = System.Web.HttpContext.Current.Server.MapPath("/WXPLOGS/" + date + "/");

            if (Directory.Exists(application) == false)
            //如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(application);
            }

            var fileName = application + "wxp" + ".txt";
            FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            try
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    //开始写入               
                    string str = time + "----" + input;
                    sw.WriteLine(str);

                    //清空缓冲区
                    sw.Flush();
                    sw.Close();
                    //关闭流
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                fs.Close();
            }
        }
        #endregion

        #region 生成订单
        /// <summary>
        /// 生成订单
        /// </summary>
        public bool CreateOrder(string productId, string buyUserId, string code, string tranId)
        {
            try
            {
                //执行sql
                using (var x = Join.Dal.MySqlProvider.X())
                {
                    //查询购买人信息
                    var selectUserSql = "select * from A_USER where kid='" + buyUserId + "'";
                    var selectUserTables = x.ExecuteSqlCommand(selectUserSql);
                    var userRows = selectUserTables.Tables[0].Rows;
                    //查询产品信息
                    var selectProductSql = "select * from B_PRODUCT_LIST where kid='" + productId + "'";
                    var selectProductTables = x.ExecuteSqlCommand(selectProductSql);
                    var productRows = selectProductTables.Tables[0].Rows;
                    if (productRows.Count > 0 && userRows.Count > 0)
                    {
                        var product = productRows[0];
                        var productName = product["NAME"];
                        var user = userRows[0];
                        var keys = @"`CODE`,
                                    `IS_DELETE`,
                                    `CRT_TIME`,
                                    `STATUS`,
                                    `TYPE`,
                                    `GAME_PARTITION_KID`,
                                    `GAME_PARTITION_NAME`,
                                    `GAME_SECONDARY_KID`,
                                    `GAME_SECONDARY_NAME`,
                                    `GAME_ZONE_KID`,
                                    `GAME_ZONE_NAME`,
                                    `BUY_USER_ID`,
                                    `BUY_USER_NAME`,
                                    `BUY_USER_PHONE`,
                                    `SELL_USER_ID`,
                                    `SELL_USER_NAME`,
                                    `SELL_USER_PHONE`,
                                    `PRODUCT_NAME`,
                                    `PRODUCT_ID`,
                                    `PRICE`,
                                    `PHOTO_URL`,
                                    `DESC_PHOTO`,
                                    `NEED_LEVEL`,
                                    `DESCRIPTION`,
                                    `ORDER_TIME`,
                                    `PAY_STATUS`,
                                    `TRAN_ID`";
                        var values = string.Format(@"'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}'",
                                            code, "0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "待发货", "商品",
                                            product["GAME_PARTITION_KID"], product["GAME_PARTITION_NAME"], product["GAME_SECONDARY_KID"]
                                            , product["GAME_SECONDARY_NAME"], product["GAME_ZONE_KID"], product["GAME_ZONE_NAME"]
                                            , user["KID"], user["NAME"], user["PHONE"], product["SELL_USER_ID"], product["SELL_USER_NAME"]
                                            , product["SELL_USER_PHONE"], product["NAME"], product["KID"], product["PRICE"], product["PHOTO_URL"]
                                            , product["DESC_PHOTO"], product["NEED_LEVEL"], product["DESCRIPTION"], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "已支付", tranId);
                        var sql = string.Format(@"insert into {0} ({1}) values ({2})", "b_order", keys, values);
                        x.ExecuteSqlCommand(sql);
                        //查询订单
                        var selectOrderSql = string.Format(@"select * from b_order where code='{0}'", code);
                        var dataTable = x.ExecuteSqlCommand(selectOrderSql);
                        if (dataTable.Tables[0].Rows.Count > 0)
                        {
                            var row = dataTable.Tables[0].Rows[0];
                            //卖家ID
                            string sellUserId = row["SELL_USER_ID"].ToString();
                            //卖家昵称
                            string sellUserName = row["SELL_USER_NAME"].ToString();
                            //卖家手机号
                            string sellUserPhone = row["SELL_USER_PHONE"].ToString();
                            //插入流水计算金额字段*0.9
                            var orderAmount = decimal.Parse(row["PRICE"].ToString());
                            //金额
                            var price = Math.Round(orderAmount * 0.9m, 2);
                            //单据ID
                            string orderId = row["KID"].ToString();

                            //下架产品
                            var updateProductSql = string.Format(@"update B_PRODUCT_LIST set status='已卖出' where kid='{0}'", productId);
                            x.ExecuteSqlCommand(updateProductSql);
                            //插入账户流水收入类型
                            var insertAccountRecordSql = string.Format(@"insert into B_ACCOUNT_RECORD (`CODE`,`USER_ID`,`USER_NAME`,`USER_PHONE`,
                                            `TYPE`,`RECEIVE_TYPE`,`SELETTMENT_STATUS`,`SELETTMENT_TIME`,`SELETTMENT_AMOUNT`,
                                            `ORDER_AMOUNT`,`ORDER_ID`,`ORDER_CODE`,`IS_DELETE`,`CRT_TIME`) values ('{0}','{1}','{2}','{3}','{4}'
                                            ,'{5}','{6}','{7}','{8}','{9}','{10}','{11}',0,'{12}')", DateTime.Now.ToString("yyyyMMddHHmmss"),
                                               sellUserId, sellUserName, sellUserPhone, "收入", "商品", "待结算", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                               price, orderAmount, orderId, code, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            x.ExecuteSqlCommand(insertAccountRecordSql);
                            //修改卖家即将收入增加
                            var upIncome = decimal.Parse(user["UPCOMING_INCOME"].ToString());
                            var amount = price;
                            //修改用户表金额
                            var resultAmount = upIncome + amount;
                            var updateUserSql = "update a_user set UPCOMING_INCOME='" + resultAmount + "' where kid='" + sellUserId + "'";
                            x.ExecuteSqlCommand(updateUserSql);
                            //执行插入消息
                            InsertMsg("发货提醒", "您的宝贝:[" + productName + "]已被拍下,请注意及时发货", sellUserId, sellUserName, sellUserPhone);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 插入消息
        /// <summary>
        /// 插入消息
        /// </summary>
        public void InsertMsg(string theme, string content, string userId, string userName, string userPhone)
        {
            using (var x = Join.Dal.MySqlProvider.X())
            {
                var insertSql = string.Format(@"insert into B_MESSAGE (`THEME`,`STATUS`,`USER_ID`,`USER_NAME`,
                                            `USER_PHONE`,`CONTENT`,`SEND_TIME`,`IS_DELETE`,`CRT_TIME`) values ('{0}','{1}','{2}','{3}','{4}'
                                            ,'{5}','{6}',0,'{7}')", theme, "待发送", userId, userName, userPhone, content
                                            , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                x.ExecuteSqlCommand(insertSql);
            }
        }
        #endregion

        #region 退款逻辑
        /// <summary>
        /// 退款逻辑
        /// </summary>
        public bool Refund(string orderNo)
        {
            try
            {
                //执行sql
                using (var x = Join.Dal.MySqlProvider.X())
                {
                    //交易关闭，退款给买家，改变订单状态为交易关闭，并且减少卖家即将收入
                    var selectOrderSql = "select * from b_order where code='" + orderNo + "' and is_delete=0 and pay_status='已支付'";
                    this.WriteLogFile("执行查询退款单据Sql：" + selectOrderSql);
                    var orderTable = x.ExecuteSqlCommand(selectOrderSql);
                    if (orderTable.Tables[0].Rows.Count > 0)
                    {
                        var orderRow = orderTable.Tables[0].Rows[0];
                        var sellUserId = orderRow["SELL_USER_ID"].ToString();
                        //金额
                        //插入流水计算金额字段*0.9
                        var orderAmount = decimal.Parse(orderRow["PRICE"].ToString());
                        //金额
                        decimal price = orderAmount * 0.9m;
                        //修改订单支付状态为已退款，状态为已关闭
                        var updateOrderSql = "update b_order set status='交易关闭' and pay_status='已退款' where code='" + orderNo + "'";
                        x.ExecuteSqlCommand(updateOrderSql);
                        //减少卖家即将收入
                        //查询购买人信息
                        var selectUserSql = "select * from A_USER where kid='" + sellUserId + "'";
                        var selectUserTables = x.ExecuteSqlCommand(selectUserSql);
                        var userRows = selectUserTables.Tables[0].Rows;
                        if (userRows.Count > 0)
                        {
                            var user = userRows[0];
                            //修改卖家即将收入减少
                            var upIncome = decimal.Parse(user["UPCOMING_INCOME"].ToString());

                            var amount = price;
                            //修改用户表金额
                            var resultAmount = upIncome - amount;
                            var updateUserSql = "update a_user set UPCOMING_INCOME='" + resultAmount + "' where kid='" + sellUserId + "'";
                            x.ExecuteSqlCommand(updateUserSql);
                        }
                        //修改流水状态为已取消
                        var updateRecordSql = "update B_ACCOUNT_RECORD set SELETTMENT_STATUS='已取消' where ORDER_CODE='" + orderNo + "'";

                    }
                    else
                    {
                        this.WriteLogFile("为查询到需要退款的单据");
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
