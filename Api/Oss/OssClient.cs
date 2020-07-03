using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TransactionAppletaApi.Oss
{
    public class OssClient
    {
        public HttpClient Client { get; private set; }

        public OssClient()
        {
            var url = System.Configuration.ConfigurationManager.AppSettings["OssService"];

            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.Client = client;
        }
        public OssClient(string url)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.Client = client;
        }
        public string GenImageUrl(string id)
        {
            return string.Format("{0}/api/oss/{1}/image", this.Client.BaseAddress, id);
        }
        public string GenUrl(string id)
        {
            return string.Format("{0}/api/oss/{1}", this.Client.BaseAddress, id);
        }
        public async Task<string> PostFile(string fileName, string fileContent)
        {
            var response = await this.Client.PostAsJsonAsync("api/oss", new { name = fileName, content = fileContent });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<string>();
                return result;
            }
            else
            {
                var bad = await response.Content.ReadAsAsync<OssBadRequest>();
                return bad.ToMessage(response.ReasonPhrase);
            }
        }

        public async Task<string> GetFile(string id)
        {
            var url = string.Format("api/oss/{0}", id);
            var response = await this.Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = response.ReasonPhrase;
                return result;
            }
            else
            {
                var bad = await response.Content.ReadAsAsync<OssBadRequest>();
                return bad.ToMessage(response.ReasonPhrase);
            }
        }

    }
}