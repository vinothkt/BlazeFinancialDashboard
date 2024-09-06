using System.Net.Http.Json;
using System.Text;

namespace BlazeMyFinance.Shared.Helper
{
    public interface IHttpFactory
    {
        Task<T?> PostApiCall<T>(string requestUri, string serializedContent);
        Task<T?> GetApiCall<T>(string requestUri);
    }

    public class HttpFactory : IHttpFactory
    {
        public readonly HttpClient _httpClient;

        public HttpFactory()
        {
            _httpClient = this.CreateClient();
        }

        private HttpClient CreateClient()
        {
            HttpClientHandler handler = new();
            var client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(600) //set your own timeout.
            };
            return client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<HttpRequestMessage> TokenizeRequest(HttpRequestMessage request)
        {
            if(request != null)
            {
                //adding the token in authorization header
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", $accessToken);
            }
            return request;
        }

        public async Task<T?> GetApiCall<T>(string requestUri)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
                requestMessage = await TokenizeRequest(requestMessage);
                var response = await _httpClient.SendAsync(requestMessage);
                var returnedData = await response.Content.ReadFromJsonAsync<T>();
                if (returnedData != null)
                    return await Task.FromResult(returnedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetApiCalls:{requestUri}: " + ex.Message + "; " + ex.StackTrace);
            }
            return default;
        }


        public async Task<T?> PostApiCall<T>(string requestUri, string serializedContent)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = new StringContent(serializedContent, Encoding.UTF8, "application/json")
                };
                requestMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                requestMessage = await TokenizeRequest(requestMessage);
                var response = await _httpClient.SendAsync(requestMessage);
                var returnedData = await response.Content.ReadFromJsonAsync<T>();
                if (returnedData != null)
                    return await Task.FromResult(returnedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PostApiCalls: {requestUri}: " + ex.Message);
            }
            return default;
        }
    }
}
