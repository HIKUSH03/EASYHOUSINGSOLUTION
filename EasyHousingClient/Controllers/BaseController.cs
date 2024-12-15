using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EasyHousingClient.Controllers
{
    public class BaseController : Controller
    {
        public static HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:54057/") // Replace with your Web API URL
        };

        public async Task<T> GetFromApi<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();

            // Deserialize using Newtonsoft.Json
            return JsonConvert.DeserializeObject<T>(contentString);
        }

        public async Task<HttpResponseMessage> PostToApi<T>(string endpoint, T data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(endpoint, content);
        }


    }
}