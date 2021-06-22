using Newtonsoft.Json;
using StarGarments.Service.Shared;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StarGarments.Service.Service.Base
{
    public class HttpServiceRepository
    {
        public HttpClient Client { get; set; }
        public HttpServiceRepository()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(GlobalConfig.BaseUrl);
        }
  
        public async Task<T> Get<T>(string url)
        {
            Client.DefaultRequestHeaders.Add("Get", "application/json");
            var result = await Client.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var readResult = await result.Content.ReadAsStringAsync();
                return readResult != null ? JsonConvert.DeserializeObject<T>(readResult) : default;
            }
            else
            {
                var readResult = await result.Content.ReadAsStringAsync();
                throw new Exception();
            }
        }

        public async Task<T> Put<T>(string url,T model)
        {
            Client.DefaultRequestHeaders.Add("PUT", "application/json");
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await Client.PutAsync(url, content);
            if (result.IsSuccessStatusCode)
            {
                return default(T);
            }
            else
            {
                var readResult = await result.Content.ReadAsStringAsync();
                throw new Exception();
            }
        }
        public async Task Post<T>(string url, T model)
        {
            Client.DefaultRequestHeaders.Add("POST", "application/json");
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await Client.PostAsync(url, content);

            if (result.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                var readResult = await result.Content.ReadAsStringAsync();
                throw new Exception();
            }
        }

        public async Task Delete(string url)
        {
            var result = await Client.DeleteAsync(url);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception();
            }
        }
    }
}
