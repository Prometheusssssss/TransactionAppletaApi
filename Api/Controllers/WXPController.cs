using Join;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
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
            var response = new HttpResponseMessage();
            WriteLogFile("TenpayOK!");
            response.Content = new StringContent(WxPayData.NotifySuccess());
            response.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("text/xml");
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
    }
}
