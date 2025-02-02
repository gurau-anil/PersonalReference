using HttpClientAndHttpClientFactory.Enums;
using HttpClientAndHttpClientFactory.Models;
using HttpClientAndHttpClientFactory.Services.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HttpClientAndHttpClientFactory.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public string apiUrl { get; set; }

        public ApiService(HttpClient httpClient, IOptions<ApiSettings> settings)
        {
            _httpClient = httpClient;
            _apiSettings = settings.Value;
            _httpClient.BaseAddress = new Uri(_apiSettings.BaseUrl);
        }

        public async Task<string> GetDataAsync()
        {
            return await SendRequestAsync(apiUrl, RequestType.Get);
        }

        public async Task<string> GetDataByIdAsync(int id)
        {
            return await SendRequestAsync(apiUrl, RequestType.Get);
        }

        public async Task<string> PostDataAsync(HttpContent content)
        {
            return await SendRequestAsync(apiUrl, RequestType.Post, content);
        }

        public async Task<string> UpdateDataAsync(int id, HttpContent content)
        {
            return await SendRequestAsync(apiUrl, RequestType.Put, content);
        }

        public async Task<string> DeleteDataAsync(int id)
        {
            return await SendRequestAsync(apiUrl, RequestType.Delete);
        }

        private async Task<string> SendRequestAsync(string url, RequestType requestType, HttpContent content = null)
        {
            HttpResponseMessage response = null;
            string retVal = String.Empty;
            switch (requestType)
            {
                case RequestType.Get:
                    response = await _httpClient.GetAsync(url);
                    break;
                case RequestType.Post:
                    response = await _httpClient.PostAsync(url, content);
                    break;
                case RequestType.Put:
                    response = await _httpClient.PutAsync(url, content);
                    break;
                case RequestType.Delete:
                    response = await _httpClient.DeleteAsync(url);
                    retVal = "Entity Deleted Successfully";
                    break;

            }
            response.EnsureSuccessStatusCode();

            retVal = String.IsNullOrEmpty(retVal) ? await response.Content.ReadAsStringAsync() : retVal;

            //Reading Raw Response Data
            return retVal;
        }
    }
}
