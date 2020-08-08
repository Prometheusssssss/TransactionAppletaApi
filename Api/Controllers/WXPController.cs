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
                var isCreate = CreateOrder(productId, buyUserId, orderNo);
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

        //#region 退款回调
        //[Route("refundApi")]
        //[AllowAnonymous]
        //[HttpPost]
        //public HttpResponseMessage RefundApi()
        //{
        //    var response = new HttpResponseMessage();
        //    try
        //    {
        //        var result = Request.Content.ReadAsStringAsync().Result;
        //        WriteLogFile("回调参数:" + result);
        //        var bizJson = WxPayData.RefundJson(result);
        //        var bizObj = JsonConvert.DeserializeObject<WxPayData.REFUND_NOTIFY>(bizJson);
        //        var status = bizObj.refund_status;
        //        if (status == "SUCCESS")
        //        {
        //            var out_refund_no = bizObj.out_refund_no;
        //            var orderNo = bizObj.out_trade_no;
        //            //拆分订单号
        //            var arrayNos = orderNo.Split('_');
        //            var wecharOrderNo = arrayNos[0];
        //            ////替换掉前缀，获取单号 2020-5-14 09:09:21
        //            //var prefix = System.Configuration.ConfigurationManager.AppSettings["TransactionPrefix"];
        //            //wecharOrderNo = wecharOrderNo.Replace(prefix, "");

        //            //金额
        //            var refundFee = bizObj.refund_fee;
        //            //拆分数据
        //            var array = out_refund_no.Split('_');
        //            var refundNo = array[0].ToString();
        //            var cid = array[1].ToString();
        //            var crtCode = array[2].ToString();
        //            var productId = "";
        //            if (array.Length > 3)
        //                productId = array[3].ToString();

        //            #region 执行回调逻辑
        //            using (var x = Join.Dal.MySqlProvider.X())
        //            {
        //                var json = new { orderNo = wecharOrderNo, cid = cid, crtCode = crtCode, amount = refundFee, productId = productId };
        //                var mySqlResult = x
        //                    .ParamIn("p", MySqlDbType.JSON, json.ToJsonString())
        //                    .ExecuteStoredProcedure("create_wechat_return_retail_account_record");
        //                x.Close();
        //                var table = mySqlResult.FirstTable();
        //                var errorCode = table.Rows[0]["ERROR_CODE"].ToString();
        //                if (errorCode != "0")
        //                {
        //                    var errorMsg = table.Rows[0]["MSG"].ToString();
        //                    WriteLogFile("TenpaySqlEx:" + errorMsg);
        //                    response.Content = new StringContent(WxPayData.NotifyFail());
        //                    response.Content.Headers.ContentType
        //              = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
        //                    return response;
        //                }
        //                else
        //                {
        //                    //2020年6月10日15:05:56  
        //                    //如果是秒杀单，释放redis空间，修改秒杀活动已订购数量
        //                    this.WriteLogFile("create_wechat_return_retail_account_record存储过程执行结果，数量：" + table.Rows[0]["ORDERED"].ToString());
        //                    var ordered = decimal.Parse(table.Rows[0]["ORDERED"].ToString());
        //                    var buyNumber = decimal.ToInt32(ordered);
        //                    if (buyNumber > 0)
        //                    {
        //                        var seckillId = table.Rows[0]["SECKILL_ID"].ToString();
        //                        var userId = table.Rows[0]["CUSTOMER_ID"].ToString();
        //                        //释放秒杀
        //                        var str = Join.Biz.SeckillRds.TryoKill(seckillId, int.Parse(userId), buyNumber);
        //                        var strList = str.Split(':');
        //                        var isSucess = bool.Parse(strList[0]);
        //                        var remainingNum = int.Parse(strList[1]);
        //                        if (isSucess == false)
        //                            WriteLogFile("执行释放秒杀失败，失败原因：可释放数不足");
        //                        else
        //                            WriteLogFile("执行释放秒杀成功");
        //                    }
        //                }
        //            }
        //            #endregion
        //        }
        //        else
        //        {
        //            WriteLogFile("TenpayEx:" + "回调失败");
        //            response.Content = new StringContent(WxPayData.NotifyFail());
        //            response.Content.Headers.ContentType
        //      = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WriteLogFile("TenpayEx:" + ex.Message);
        //        response.Content = new StringContent(WxPayData.NotifyFail());
        //        response.Content.Headers.ContentType
        //          = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
        //        return response;
        //    }
        //    WriteLogFile("TenpayOK!");
        //    response.Content = new StringContent(WxPayData.NotifySuccess());
        //    response.Content.Headers.ContentType
        //            = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
        //    return response;
        //}
        //#endregion

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
        public bool CreateOrder(string productId, string buyUserId, string code)
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
                        var productName = product["productName"];
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
                                    `ORDER_TIME`";
                        var values = string.Format(@"'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}'",
                                            code, "0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "待发货", "商品",
                                            product["GAME_PARTITION_KID"], product["GAME_PARTITION_NAME"], product["GAME_SECONDARY_KID"]
                                            , product["GAME_SECONDARY_NAME"], product["GAME_ZONE_KID"], product["GAME_ZONE_NAME"]
                                            , user["KID"], user["NAME"], user["PHONE"], product["SELL_USER_ID"], product["SELL_USER_NAME"]
                                            , product["SELL_USER_PHONE"], product["NAME"], product["KID"], product["PRICE"], product["PHOTO_URL"]
                                            , product["DESC_PHOTO"], product["NEED_LEVEL"], product["DESCRIPTION"], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
                            //金额
                            string price = row["PRICE"].ToString();
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
                                               price, price, orderId, code, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            x.ExecuteSqlCommand(insertAccountRecordSql);
                            //修改卖家即将收入增加
                            var upIncome = decimal.Parse(user["UPCOMING_INCOME"].ToString());
                            var amount = decimal.Parse(price);
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
    }
}
