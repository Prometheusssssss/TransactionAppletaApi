using Join;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace TransactionAppletaApi
{
    internal class WxPayData
    {
        #region A.成员变量
        //采用排序的Dictionary的好处是方便对数据包进行签F名，不用再签名之前再做一次排序
        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();
        #endregion

        #region F.工厂方法
        public static WxPayData ForApplets(double price, string openId, string orderNo, string ip, string attach)
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["notify_url"];
            var fee = Convert.ToInt32(price * 100);
            var result = new WxPayData();
            result.SetValue("trade_type", "JSAPI");//交易类型          
            result.SetValue("appid", GlobalVariableWeChatApplets.APPID);
            result.SetValue("mch_id", GlobalVariableWeChatApplets.MCH_ID);//商户号
            result.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            result.SetValue("body", "小程序下单");//商品描述
            result.SetValue("out_trade_no", orderNo);//订单号
            result.SetValue("total_fee", fee);//总金额
            result.SetValue("openid", openId);//用户openId	
            result.SetValue("attach", attach);//附加数据
            result.SetValue("spbill_create_ip", ip);//终端ip	
            result.SetValue("notify_url", url + "/api/_wxp/tenpay_notify");//异步通知url
            result.SetValue("sign_type", "HMAC-SHA256"); //签名类型
            result.SetValue("sign", result.WechatMakeSignByHMAC_SHA256()); //签名
            result.WriteLogFile("ForApplets:" + result.ToJson());
            return result;
        }


        public static WxPayData ForWechatPay(WxPayData preOrder)
        {
            var result = new WxPayData();

            result.SetValue("appId", preOrder.GetValue("appid"));//公众账号ID
            result.SetValue("mch_id", preOrder.GetValue("mch_id"));//小程序APPId
            result.SetValue("package", "prepay_id=" + preOrder.GetValue("prepay_id"));//订单详情扩展字符串prepay_id
            result.SetValue("nonceStr", GenerateNonceStr());//随机字符串
            result.SetValue("timeStamp", ConvertDateTimeInt(DateTime.Now));//时间戳
            result.SetValue("signType", "MD5");
            result.SetValue("paySign", result.WechatMakeSign());//签名
            result.WriteLogFile("ForWechatPay:" + result.ToJson());
            return result;
        }

        /// <summary>
        /// 退款
        /// </summary>
        public static WxPayData ForRefund(double price, double refundPrice, string orderNo, string refundNo, string subMchId, string apiUrl, string subAppId)
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["notify_url"];
            var fee = Convert.ToInt32(price * 100);
            var refundPriceFee = Convert.ToInt32(refundPrice * 100);
            var result = new WxPayData();
            result.SetValue("appid", GlobalVariableWeChatApplets.APPID);//服务商的APPID
            result.SetValue("mch_id", GlobalVariableWeChatApplets.MCH_ID);//商户号
            result.SetValue("sub_appid", subAppId);//小程序的APPID
            result.SetValue("sub_mch_id", subMchId);//子商户号
            result.SetValue("nonce_str", GenerateNonceStr());//随机字符串
            result.SetValue("out_trade_no", orderNo);//订单号
            result.SetValue("out_refund_no", refundNo);// "{'cid':" + cid + ",'crt_code':" + loginCode + ",'refNo':" + refundNo + " }");//商户退款单号（拼接CID CODE）
            result.SetValue("total_fee", fee);//订单金额
            result.SetValue("refund_fee", refundPriceFee);//退款金额
            result.SetValue("notify_url", url + apiUrl + "/api/_wxp/refundApi");//退款通知url
            //签名
            result.SetValue("sign", result.WechatMakeSign()); //签名
            result.WriteLogFile("退款Json:" + result.ToJson());
            return result;
        }

        public static WxPayData ForWechatRefund(WxPayData preOrder)
        {
            var result = new WxPayData();
            result.SetValue("appId", preOrder.GetValue("appid"));//公众账号ID
            result.SetValue("nonceStr", GenerateNonceStr());//随机字符串
            result.SetValue("timeStamp", ConvertDateTimeInt(DateTime.Now));//时间戳
            result.SetValue("signType", "MD5");
            result.SetValue("paySign", result.WechatMakeSign());//签名
            result.WriteLogFile("ForWechatPay:" + result.ToJson());
            return result;
        }

        public static WxPayData FromNoSignXml(string xml)
        {
            var result = new WxPayData();
            result.LoadNosignXml(xml);
            return result;
        }

        /// <summary>
        /// 返回结果转换对象
        /// </summary>
        public static WxPayData FromXml(string xml, string type = "MD5")
        {
            var result = new WxPayData();
            result.LoadXml(xml, type);
            return result;
        }

        public static WxPayData ForSuccess()
        {
            var result = new WxPayData();

            result.SetValue("return_code", "SUCCESS");//公众账号ID
            result.SetValue("return_msg", "支付成功");
            return result;
        }

        public static WxPayData ForFail()
        {
            var result = new WxPayData();

            result.SetValue("return_code", "FAIL");//公众账号ID
            result.SetValue("return_msg", "支付失败");
            return result;
        }

        //转换消息
        public static string ParaseNotify(string data)
        {
            var sales_no = "";
            var total = "0";
            var transaction_id = "";
            string attach = null;
            var notifyData = FromXml(data, "HMAC-SHA256");
            if (notifyData.CheckSign("HMAC-SHA256"))
            //if(true)
            {
                if (notifyData.GetValue("return_code").ToString() == "SUCCESS")
                {
                    if (notifyData.GetValue("result_code").ToString() == "SUCCESS")
                    {
                        sales_no = (string)notifyData.GetValue("out_trade_no");
                        total = (string)notifyData.GetValue("total_fee");
                        transaction_id = (string)notifyData.GetValue("transaction_id");
                        attach = (string)notifyData.GetValue("attach");
                        string sa = total;

                        if (total.Length <= 2)
                        {
                            if (total.Length == 0) { sa = "0.00"; }
                            if (total.Length == 1) { sa = "0.0" + total; }
                            if (total.Length == 2) { sa = "0." + total; }
                        }
                        else
                        {
                            string se = total.Substring(total.Length - 2, 2);
                            string ss = total.Substring(0, total.Length - 2);
                            sa = ss + "." + se;
                        }
                        total = sa;

                    }
                }
                else if (notifyData.GetValue("return_code").ToString() == "FAIL")
                {
                    var msg = (string)notifyData.GetValue("return_msg");
                    throw new Exception(msg);
                }
            }
            else
            {
                var msg = "签名失败";
                throw new Exception(msg);
            }

            return LitJson.JsonMapper.ToJson(new { sales_no = sales_no, total = total, arrach = attach, transaction_id = transaction_id });

        }

        #region 退款转换Json
        //转换消息
        public static string RefundJson(string data)
        {
            var lastResult = "";
            var req_info = "";
            var notifyData = FromNoSignXml(data);
            var m_values = new SortedDictionary<string, object>();
            //if (notifyData.CheckSign())
            //if(true)
            {
                if (notifyData.GetValue("return_code").ToString() == "SUCCESS")
                {
                    req_info = (string)notifyData.GetValue("req_info");
                    //MD5加密
                    var appKey = GlobalVariableWeChatApplets.API_KEY;
                    var md5 = MD5.Create();
                    var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(appKey));
                    var sb = new StringBuilder();
                    foreach (byte b in bs)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    //所有字符转为大写
                    var resultMd5 = sb.ToString().ToLower();
                    //解密
                    byte[] keyArray = UTF8Encoding.UTF8.GetBytes(resultMd5);
                    byte[] toEncryptArray = Convert.FromBase64String(req_info);
                    RijndaelManaged rDel = new RijndaelManaged();
                    rDel.Key = keyArray;
                    rDel.Mode = CipherMode.ECB;
                    rDel.Padding = PaddingMode.PKCS7;
                    ICryptoTransform cTransform = rDel.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    lastResult = UTF8Encoding.UTF8.GetString(resultArray);
                    lastResult = lastResult.Replace("root", "xml");

                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(lastResult);
                    var xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
                    var nodes = xmlNode.ChildNodes;
                    foreach (var xn in nodes)
                    {
                        var xe = (XmlElement)xn;
                        m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
                    }
                }
                else if (notifyData.GetValue("return_code").ToString() == "FAIL")
                {
                    var msg = (string)notifyData.GetValue("return_msg");
                    throw new Exception(msg);
                }
            }
            //else
            //{
            //    var msg = "签名失败";
            //    throw new Exception(msg);
            //}
            object out_refund_no = null;
            object out_trade_no = null;
            object refund_status = null;
            object refund_fee = null;
            m_values.TryGetValue("out_refund_no", out out_refund_no);
            m_values.TryGetValue("out_trade_no", out out_trade_no);
            m_values.TryGetValue("refund_status", out refund_status);
            m_values.TryGetValue("refund_fee", out refund_fee);

            var refundFee = refund_fee.ToString();
            string sa = refund_fee.ToString();

            if (refundFee.Length <= 2)
            {
                if (refundFee.Length == 0) { sa = "0.00"; }
                if (refundFee.Length == 1) { sa = "0.0" + refundFee; }
                if (refundFee.Length == 2) { sa = "0." + refundFee; }
            }
            else
            {
                string se = refundFee.Substring(refundFee.Length - 2, 2);
                string ss = refundFee.Substring(0, refundFee.Length - 2);
                sa = ss + "." + se;
            }
            refundFee = sa;

            return LitJson.JsonMapper.ToJson(new { out_refund_no = out_refund_no, out_trade_no = out_trade_no, refund_status = refund_status, refund_fee = refundFee });

        }
        #endregion

        //获取订单类型
        public static string GetOrderType(string orderNo)
        {
            var type = System.Configuration.ConfigurationManager.AppSettings[orderNo];
            if (type != null)
                return type;
            return string.Empty;
        }
        //通知错误
        public static string NotifyFail()
        {
            var result = ForFail();
            return result.ToXml();
        }
        //通知成功
        public static string NotifySuccess()
        {
            var result = ForSuccess();
            return result.ToXml();
        }

        #region 获取OPEN_ID
        /// <summary>
        /// 获取OPEN_ID
        /// </summary>
        public static jscode2session_REP GetOpenId(string jsCode)
        {
            var appId = GlobalVariableWeChatApplets.APPID;
            var appSecret = GlobalVariableWeChatApplets.APP_SECRET;
            var url = string.Format(@"https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code", appId, appSecret, jsCode);
            HttpClient client = new HttpClient();
            var data = client.GetStringAsync(url).Result;
            return data.FromJsonObject<jscode2session_REP>();
        }
        #endregion

        #endregion

        #region K.构造方法
        public WxPayData() { }
        #endregion

        #region X.成员方法
        /**
        * 设置某个字段的值
        * @param key 字段名
         * @param value 字段值
*/
        public void SetValue(string key, object value)
        {
            m_values[key] = value;
        }

        /**
        * 根据字段名获取某个字段的值
        * @param key 字段名
         * @return key对应的字段值
*/
        public object GetValue(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            return o;
        }
        /**
        * 判断某个字段是否已设置
        * @param key 字段名
        * @return 若字段key已被设置，则返回true，否则返回false
*/
        public bool IsSet(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }

        /**
        * @将Dictionary转成xml
        * @return 经转换得到的xml串
        * @throws WxPayException
        **/
        public string ToXml()
        {

            //数据为空时不能转化为xml格式
            if (0 == m_values.Count)
            {
                //Log.Error(this.GetType().ToString(), "WxPayData数据为空!");
                throw new Exception("WxPayData数据为空!");
            }

            string xml = "<xml>";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                WriteLogFile("1");
                WriteLogFile(pair.ToString());

                //字段值不能为null，会影响后续流程
                if (pair.Value == null)
                {
                    //WriteLogFile("1");
                    //WriteLogFile(pair.ToString());
                    //Log.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                if (pair.Value.GetType() == typeof(int))
                {
                    xml += "<" + pair.Key + ">" + pair.Value + "</" + pair.Key + ">";
                }
                else if (pair.Value.GetType() == typeof(string))
                {
                    xml += "<" + pair.Key + ">" + "<![CDATA[" + pair.Value + "]]></" + pair.Key + ">";
                }
                else//除了string和int类型不能含有其他数据类型
                {
                    //Log.Error(this.GetType().ToString(), "WxPayData字段数据类型错误!");
                    throw new Exception("WxPayData字段数据类型错误!");
                }
            }
            xml += "</xml>";
            return xml;
        }

        /**
        * @将xml转为WxPayData对象并返回对象内部的数据
        * @param string 待转换的xml串
        * @return 经转换得到的Dictionary
        * @throws WxPayException
*/
        public SortedDictionary<string, object> LoadXml(string xml, string type = "MD5")
        {
            this.WriteLogFile(xml);
            if (string.IsNullOrEmpty(xml))
            {
                //Log.Error(this.GetType().ToString(), "将空的xml串转换为WxPayData不合法!");
                throw new Exception("将空的xml串转换为WxPayData不合法!");
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            var nodes = xmlNode.ChildNodes;
            foreach (var xn in nodes)
            {
                var xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }

            try
            {
                //2015-06-29 错误是没有签名
                if (m_values["return_code"].ToString() != "SUCCESS")
                {
                    if (m_values.ContainsKey("result_code") && m_values["result_code"].ToString() == "FAIL")
                        throw new Exception(m_values["err_code"] + ":" + m_values["err_code_des"]);
                    else return m_values;
                }
                CheckSign(type);//验证签名,不通过会抛异常
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return m_values;
        }

        public SortedDictionary<string, object> LoadNosignXml(string xml)
        {
            this.WriteLogFile(xml);
            if (string.IsNullOrEmpty(xml))
            {
                //Log.Error(this.GetType().ToString(), "将空的xml串转换为WxPayData不合法!");
                throw new Exception("将空的xml串转换为WxPayData不合法!");
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            var nodes = xmlNode.ChildNodes;
            foreach (var xn in nodes)
            {
                var xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;//获取xml的键值对到WxPayData内部的数据中
            }

            try
            {
                //2015-06-29 错误是没有签名
                if (m_values["return_code"].ToString() != "SUCCESS")
                {
                    if (m_values.ContainsKey("result_code") && m_values["result_code"].ToString() == "FAIL")
                        throw new Exception(m_values["err_code"] + ":" + m_values["err_code_des"]);
                    else return m_values;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return m_values;
        }

        /**
        * @Dictionary格式转化成url参数格式
        * @ return url格式串, 该串不包含sign字段值
*/
        public string ToUrl()
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                WriteLogFile("2");
                WriteLogFile(pair.ToString());
                if (pair.Value == null)
                {
                    //Log.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                    //WriteLogFile("2");
                    //WriteLogFile(pair.ToString());
                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }


        /**
        * @Dictionary格式化成Json
         * @return json串数据
*/
        public string ToJson()
        {

            string jsonStr = JsonMapper.ToJson(m_values);
            return jsonStr;
        }

        /**
        * @values格式化成能在Web页面上显示的结果（因为web页面上不能直接输出xml格式的字符串）
*/
        public string ToPrintStr()
        {
            string str = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                WriteLogFile("3");
                WriteLogFile(pair.ToString());
                if (pair.Value == null)
                {
                    //Log.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");

                    throw new Exception("WxPayData内部含有值为null的字段!");
                }

                str += string.Format("{0}={1}<br>", pair.Key, pair.Value.ToString());
            }
            //Log.Debug(this.GetType().ToString(), "Print in Web Page : " + str);
            return str;
        }

        /**
        * @生成签名，详见签名生成算法
        * @return 签名, sign字段不参加签名
*/
        public string WechatMakeSign()
        {
            //转url格式
            string str = ToUrl();
            //在string后加入API KEY
            str += "&key=" + GlobalVariableWeChatApplets.API_KEY;
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// 签名 HMAC-SHA256
        /// </summary>
        /// <returns></returns>
        public string WechatMakeSignByHMAC_SHA256()
        {
            //转url格式
            string str = ToUrl();
            //在string后加入API KEY
            str += "&key=" + GlobalVariableWeChatApplets.API_KEY;
            var secret = GlobalVariableWeChatApplets.API_KEY ?? "";
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(str);
            var hmacsha256 = new HMACSHA256(keyByte);
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashmessage.Length; i++)
            {
                builder.Append(hashmessage[i].ToString("x2"));
            }
            return builder.ToString().ToUpper();
        }
        /**
        * 
        * 检测签名是否正确
        * 正确返回true，错误抛异常
*/
        public bool CheckSign(string type = "MD5")
        {
            //如果没有设置签名，则跳过检测
            if (!IsSet("sign"))
            {
                //Log.Error(this.GetType().ToString(), "WxPayData签名存在但不合法!");
                throw new Exception("WxPayData签名存在但不合法!");
            }
            //如果设置了签名但是签名为空，则抛异常
            else if (GetValue("sign") == null || GetValue("sign").ToString() == "")
            {
                //Log.Error(this.GetType().ToString(), "WxPayData签名存在但不合法!");
                throw new Exception("WxPayData签名存在但不合法!");
            }

            //获取接收到的签名
            string return_sign = GetValue("sign").ToString();

            //在本地计算新的签名
            string sign = "";
            if (type == "MD5")
                sign = WechatMakeSign();
            else
                sign = WechatMakeSignByHMAC_SHA256();
            if (sign == return_sign)
                return true;
            //Log.Error(this.GetType().ToString(), "WxPayData签名验证错误!");
            throw new Exception("WxPayData签名验证错误!");
        }

        /**
        * @获取Dictionary
*/
        public SortedDictionary<string, object> GetValues()
        {
            return m_values;
        }
        #endregion

        #region Y.内部方法
        public static string GenerateNonceStr()
        {
            return Guid.NewGuid().ToString("N");
        }
        public static string ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return ((int)(time - startTime).TotalSeconds).ToString();
        }
        #endregion

        #region Model

        public class ResultInfo
        {
            public bool IsSuccess { get; set; }
            public string ErroMessage { get; set; }
            public string Json { get; set; }
        }

        public class NOTIFY
        {
            public string SALES_NO { get; set; }
            public string TOTAL { get; set; }
            public string ARRACH { get; set; }
            public string TRANSACTION_ID { get; set; }
        }

        public class REFUND_NOTIFY
        {
            //订单号
            public string out_trade_no { get; set; }
            //退款单号
            public string out_refund_no { get; set; }
            //状态
            public string refund_status { get; set; }
            //金额
            public string refund_fee { get; set; }
        }

        //获取OPEN_ID
        public class jscode2session_REP
        {
            public string openid { get; set; }
            public string session_key { get; set; }
            public string unionid { get; set; }
        }

        public class AttachModel
        {
            public string spName { get; set; }
            public string apiUrl { get; set; }
        }
        #endregion

        //写入log方法Server.MapPath("../")
        public void WriteLogFile(string input)
        {
            var fileName = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
            //var fileName = System.Web.HttpContext.Current.Server.MapPath("~/log.txt");
            FileStream fs = new FileStream(fileName, FileMode.Append);

            //FileStream fs = new FileStream("D:/sh.sun.jing/log.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                //开始写入
                sw.Write(DateTime.Now.ToString());
                sw.Write("-");
                sw.Write(input);
                sw.Write("\r\n");

                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                Console.WriteLine("写入成功");
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

    }
}
