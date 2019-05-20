using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UsersAlbumsService.Models;

namespace UsersAlbumsService
{
    public class DataServiceHttpClient
    {
        private static readonly HttpClient Client;

        static DataServiceHttpClient()
        {
            Client = new HttpClient();
        }

        public DataServiceHttpClient(IConfiguration configuration)
        {
            Client.BaseAddress = new Uri(configuration.GetSection("data-service-base-address").Value);
        }

        public async Task<T> SendRequestAsync<T>(HttpMethod httpMethod, string requestUrl)
        {
            var request = new HttpRequestMessage(httpMethod, requestUrl);
            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await ReadAsAsync<T>(response);
        }

        protected static async Task<TResult> ReadAsAsync<TResult>(HttpResponseMessage response)
        {
            return JsonConvert.DeserializeObject<TResult>(await response.Content.ReadAsStringAsync());
        }
    }
}