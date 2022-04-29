using IEduZimAPI.Models.Enums;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IEduZimAPI.Services
{
    public class HttpClientService : IHttpClientService
    {
        public IHttpClientFactory HttpClient { get; set; }

        public HttpClientService(IHttpClientFactory httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<string> SendAsync(string url)
        {

            var client = HttpClient.CreateClient("PaynowAPI");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(url);
            client.DefaultRequestHeaders.Clear();

            HttpResponseMessage apiResponse = null;

            message.Method = HttpMethod.Post;

            apiResponse = await client.SendAsync(message);
            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            return apiContent;
        }
    }
}