using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace WechatRouteApi.Controllers
{

    [RoutePrefix("api/_search")]
    public class SearchController : ApiController
    {
        #region X.成员方法[计算]
        /// <summary>
        /// 使用Model Data获取数据
        /// </summary>
        [HttpPost]
        [Route("dzmnq")]
        public object Post([FromBody]JToken json)
        {
            var jsonDy = json as dynamic;
            return null;
        }
        #endregion

        /// <summary>
        /// 使用Model Data获取数据
        /// </summary>
        [HttpGet]
        [Route("test/{type}")]
        public string GetTest(string type)
        {
            return type;
        }
    }
}
