using Join;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace TransactionAppletaApi
{
    [RoutePrefix("api/_cud")]
    //[Security.AuthorizationRequired]
    public class CudController : BaseController
    {
        /// <summary>
        /// 插入 
        /// http://localhost:64665/api/_cud/createAndUpdate/tableName
        /// </summary>
        [HttpPost]
        [Route("createAndUpdate/{tableName}")]
        public IHttpActionResult GetTable(string tablename, [FromBody]JToken json)
        {
            return this.TryReturn<object>(() =>
            {
                try
                {
                    //执行sql
                    using (var x = Join.Dal.MySqlProvider.X())
                    {
                        var dicJson = json.ToJsonString();
                        var dict = dicJson.JsonToDictionary();
                        var kid = dict.GetValue("KID");
                        var sql = "";
                        //new
                        if (kid == "-1")
                        {
                            var ra = new Random();
                            var keys = "`CODE`,`IS_DELETE`";
                            var values = "'" + DateTime.Now.ToString("yyyyMMddHHmmss") + ra.Next(1000, 9999) + "',0";
                            foreach (var item in dict)
                            {
                                if (item.Key != "KID")
                                {
                                    keys = keys.AppendSql(item.Key, "`", true);
                                    values = values.AppendSql(item.Value.ToString(), "'", true);
                                }
                            }
                            sql = string.Format(@"insert into {0} ({1}) values ({2})", tablename, keys, values);
                        }
                        //update
                        else
                        {
                            var updateSql = "";
                            foreach (var item in dict)
                            {
                                if (item.Key != "KID")
                                {
                                    if (updateSql != "")
                                        updateSql = updateSql + "`" + item.Key + "`='" + item.Value.ToString() + "',";
                                    else
                                        updateSql = "`" + item.Key + "`='" + item.Value.ToString() + "',";
                                }
                            }
                            updateSql = updateSql.Substring(0, updateSql.Length - 1);

                            sql = string.Format(@"update {0} set {1} where kid='{2}'", tablename, updateSql, kid);
                        }
                        var dt = x.ExecuteSqlCommand(sql);
                        //执行扩展逻辑
                        switch (tablename)
                        {
                            case "b_order":
                                ExcuteInsertOrderEx(kid);
                                break;
                            default:
                                break;
                        }
                        return new { Table = dt, IS_SUCCESS = true, MSG = "" };
                    }
                }
                catch (Exception ex)
                {
                    return new { Table = "", IS_SUCCESS = false, MSG = ex.Message };
                }
            });
        }

        #region 成员方法

        #region 生成单据扩展逻辑
        /// <summary>
        /// 生成单据扩展逻辑
        /// </summary>
        /// <param name="kid"></param>
        public void ExcuteInsertOrderEx(string kid)
        {
            //执行
        }
        #endregion

        #endregion
    }
}
