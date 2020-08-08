using Join;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace TransactionAppletaApi
{
    [RoutePrefix("api/_login")]
    //[Security.AuthorizationRequired]
    public class LoginController : BaseController
    {
        /// <summary>
        /// 校验是否自动登录  JSCODE登录
        /// http://localhost:64665/api/_login/doAutoLogin
        /// </summary>
        [HttpPost]
        [Route("doAutoLogin")]
        public IHttpActionResult DoAutoLogin([FromBody]JToken json)
        {
            return this.TryReturn<object>(() =>
            {
                try
                {
                    var jsn = json.AsDynamic();
                    string jsCode = jsn.jsCode;
                    //根据JsCode换取OpenId
                    var jsCode2Session = WxPayData.GetOpenId(jsCode);
                    var openId = jsCode2Session.openid;
                    if (openId == "")
                    {
                        return new { Table = "", IS_SUCCESS = false, MSG = "JsCode失效，未获取到OpenId" };
                    }
                    else
                    {
                        using (var x = Join.Dal.MySqlProvider.X())
                        {
                            //根据OPEN_ID去数据库查询登录信息
                            var searchSql = "select * from a_user where open_id='" + openId + "'";
                            var openIdDt = x.ExecuteSqlCommand(searchSql);
                            x.Close();
                            //如果根据OPEN_ID查询不到，不能自动登录
                            if (openIdDt.Tables[0].Rows.Count > 0)
                            {
                                return new { Table = openIdDt.Tables[0], IS_SUCCESS = true, MSG = "" };
                            }
                            else
                            {
                                return new { Table = "", IS_SUCCESS = false, MSG = "请先登录/注册" };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new { Table = "", IS_SUCCESS = false, MSG = ex.Message };
                }
            });
        }

        /// <summary>
        /// 手机号登录/注册
        /// http://localhost:64665/api/_login/doLogin
        /// </summary>
        [HttpPost]
        [Route("doLogin")]
        public IHttpActionResult DoLogin([FromBody]JToken json)
        {
            return this.TryReturn<object>(() =>
            {
                try
                {
                    var jsn = json.AsDynamic();
                    string jsCode = jsn.jsCode;
                    string baseName = jsn.name;
                    var baseStr = Convert.FromBase64String(baseName);
                    var name = Encoding.UTF8.GetString(baseStr);
                    string url = jsn.url;
                    string encryptedData = jsn.encryptedData;
                    string iv = jsn.iv;
                    //根据JsCode换取OpenId
                    var jsCode2Session = WxPayData.GetOpenId(jsCode);
                    var openId = jsCode2Session.openid;
                    if (openId == "" || openId == null)
                    {
                        return new { Table = "", IS_SUCCESS = false, MSG = "JsCode失效，未获取到OpenId" };
                    }
                    else
                    {
                        //解密手机号
                        var wxModel = DescodeWxSHA1(encryptedData, jsCode2Session.session_key, iv);
                        var tel = wxModel.PhoneNumber;
                        using (var x = Join.Dal.MySqlProvider.X())
                        {
                            //根据手机号去数据库查询登录信息
                            var searchSql = "select * from a_user where is_delete=0 and PHONE='" + tel + "'";
                            var dt = x.ExecuteSqlCommand(searchSql);
                            //如果查询到数据，更新OPEN_ID
                            if (dt.Tables[0].Rows.Count > 0)
                            {
                                var kid = dt.Tables[0].Rows[0]["KID"].ToString();
                                //执行插入OPEN_ID
                                var updateSql = string.Format(@"update a_user set open_id='{0}' where kid='{1}'", openId, kid);
                                x.ExecuteSqlCommand(updateSql);
                            }
                            //如果根据手机号查询不到,执行创建用户
                            else
                            {
                                //手机号后4位
                                var password = tel.Substring(tel.Length - 4, 4);
                                var insertSql = string.Format(@"insert into a_user (`CODE`,`NAME`,`PHONE`,`OPEN_ID`,`PASSWORD`,`IMG_URL`,`REGIST_DATE`,`IS_DELETE`,`IS_SA`) 
                                                            values('{0}','{1}','{2}','{3}','{4}','{5}','{6}',0,0)"
                                                            , DateTime.Now.ToString("yyyyMMddHHmmss"), name, tel, openId,
                                                            password, url, DateTime.Now.ToString("yyyy-MM-dd"));
                                x.ExecuteSqlCommand(insertSql);
                            }
                            //根据手机号去数据库查询登录信息
                            var searchResultSql = "select * from a_user where is_delete=0 and PHONE='" + tel + "'";
                            var sdt = x.ExecuteSqlCommand(searchResultSql);
                            x.Close();
                            return new { Table = sdt.Tables[0], IS_SUCCESS = true, MSG = "" };
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new { Table = "", IS_SUCCESS = false, MSG = ex.Message };
                }
            });
        }

        #region 解密小程序的encryptedData
        /// <summary>
        /// 解密小程序的encryptedData
        /// </summary>
        /// <param name="encryptedData">加密的信息</param>
        /// <param name="sessionKey">key</param>
        /// <param name="iv">加密算法的初始向量</param>
        public static WxPhoneModel DescodeWxSHA1(string encryptedData, string sessionKey, string iv)
        {
            WxPhoneModel model = null;
            var res = AESDecrypt(encryptedData, sessionKey, iv);// {"phoneNumber":"152XXXX9583","purePhoneNumber":"1525XXXX3","countryCode":"86","watermark":{"timestamp":1525829586,"appid":"wx38XXXXXXXX43"}} 
            if (!string.IsNullOrEmpty(res))
            {
                model = JObject.Parse(res).ToObject<WxPhoneModel>();
            }
            return model;
        }

        public static string AESDecrypt(string encryptedData, string sessionKey, string iv)
        {
            try
            {
                //16进制数据转换成byte
                var encryptedDataByte = Convert.FromBase64String(encryptedData);  // strToToHexByte(text);
                var rijndaelCipher = new RijndaelManaged
                {
                    Key = Convert.FromBase64String(sessionKey),
                    IV = Convert.FromBase64String(iv),
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                };

                var transform = rijndaelCipher.CreateDecryptor();
                var plainText = transform.TransformFinalBlock(encryptedDataByte, 0, encryptedDataByte.Length);
                var result = Encoding.Default.GetString(plainText);

                return result;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public class WxPhoneModel
        {
            /// <summary>
            /// 用户绑定的手机号（国外手机号会有区号）
            /// </summary>
            public string PhoneNumber { set; get; }

            /// <summary>
            /// 没有区号的手机号
            /// </summary>
            public string PurePhoneNumber { set; get; }

            /// <summary>
            /// 区号
            /// </summary>
            public string CountryCode { set; get; }

            /// <summary>
            /// 水印
            /// </summary>
            public WaterMarkModel WaterMark { set; get; }
        }

        public class WaterMarkModel
        {
            /// <summary>
            /// appid
            /// </summary>
            public string AppId { set; get; }

            /// <summary>
            /// 时间戳
            /// </summary>
            public string TimeStamp { set; get; }
        }
        #endregion
    }
}
