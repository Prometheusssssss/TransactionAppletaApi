using Join;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Http;

namespace TransactionAppletaApi
{
    [RoutePrefix("api/_search")]
    //[Security.AuthorizationRequired]
    public class SearchController : BaseController
    {
        #region X.成员方法[计算]
        /// <summary>
        /// 使用Model Data获取数据
        /// </summary>
        [HttpGet]
        [Route("test/{str}")]
        public string Post(string str)
        {
            return str;
        }
        #endregion

        /// <summary>
        /// 通用查询表 
        /// http://localhost:64665/api/_search/defaultSearch/a_game_setting?filter={"PARENT_ID":null}
        /// </summary>
        [HttpGet]
        [Route("defaultSearch/{tableName}")]
        public IHttpActionResult GetTable(string tableName, string filter)
        {
            return this.TryReturn<object>(() =>
            {
                try
                {
                    var wsql = "";
                    if (filter != "{}")
                    {
                        var jsn = filter.ToJToken();
                        wsql = jsn.ToWhereSql();
                    }
                    var backResult = SearchHelper.SearchTable(tableName, wsql);
                    return new { Table = backResult.Tables[0], IS_SUCCESS = true, MSG = "" };

                }
                catch (Exception ex)
                {
                    return new { Table = "", IS_SUCCESS = false, MSG = ex.Message };
                }
            });
        }

        /// <summary>
        /// POST获取数据
        /// </summary>
        [HttpPost]
        [Route("postSearch")]
        public IHttpActionResult PostSearch([FromBody]JToken json)
        {
            return this.TryReturn<object>(() =>
            {
                try
                {
                    var jtoken = json.AsDynamic();
                    string tableName = jtoken.tableName;
                    int page = jtoken.page;
                    int limit = jtoken.limit;
                    JToken filter = jtoken.filters;
                    var wsql = filter.ToFilterSql();
                    //构造take
                    var backResult = SearchHelper.SearchTable(tableName, wsql, page, limit);
                    return new { Table = backResult.Tables[0], IS_SUCCESS = true, MSG = "" };
                }
                catch (Exception ex)
                {
                    return new { Table = "", IS_SUCCESS = false, MSG = ex.Message };
                }

            });
        }
    }
}
