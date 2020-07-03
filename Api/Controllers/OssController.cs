using Join;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace TransactionAppletaApi
{
    [RoutePrefix("api/_oss")]
    //[Security.AuthorizationRequired]
    public class OssController : BaseController
    {
        #region X.成员方法[Oss]
        [HttpPost]
        [Route("ossimg")]
        public async Task<object> OssPost()
        {
            var value = await Request.Content.ReadAsStringAsync();
            var temp = new { name = "", content = "" }.AsArray();
            var list = JsonConvert.DeserializeAnonymousType(value, temp);
            List<object> result = new List<object>();
            foreach (var de in list)
            {
                var client = new Oss.OssClient();
                var returnResult = await client.PostFile(de.name, de.content);
                string[] resultArray = returnResult.Split('|');
                var id = resultArray[0];
                var takeTime = "";
                if (resultArray.Length >= 2)
                {
                    takeTime = resultArray[1];
                }
                var url = client.GenImageUrl(id);
                result.Add(new
                {
                    name = de.name,
                    url = url,
                    size = de.content.Length,
                    takeTime = takeTime
                });
            }
            return new { Table = result, IS_SUCCESS = true, MSG = "" };
        }
        #endregion

        #region X.成员方法[Oss]
        [HttpPost]
        [Route("ossfile")]
        public async Task<object> OssFilePost()
        {
            var value = await Request.Content.ReadAsStringAsync();
            var temp = new { name = "", content = "" }.AsArray();
            var list = JsonConvert.DeserializeAnonymousType(value, temp);
            List<object> result = new List<object>();
            foreach (var de in list)
            {
                var client = new Oss.OssClient();
                var returnResult = await client.PostFile(de.name, de.content);
                string[] resultArray = returnResult.Split('|');
                var id = resultArray[0];
                var takeTime = "";
                if (resultArray.Length >= 2)
                {
                    takeTime = resultArray[1];
                }

                var url = client.GenUrl(id);

                result.Add(new
                {
                    name = de.name,
                    url = url,
                    size = de.content.Length,
                    takeTime = takeTime
                });
            }
            return result;
        }
        #endregion
    }
}
