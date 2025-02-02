using HttpClientAndHttpClientFactory.Models;
using HttpClientAndHttpClientFactory.Services.Interface;
using Newtonsoft.Json;

namespace HttpClientAndHttpClientFactory.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ApiService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public Task DeleteDataAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetDataAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("posts");
            response.EnsureSuccessStatusCode();

            //Reading Raw Response Data
            return await response.Content.ReadAsStringAsync();
        }

        public Task<PostModel> GetDataByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PostModel> PostDataAsync(PostAddModel model)
        {
            throw new NotImplementedException();
        }

        public Task PostFileDataAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<PostModel> UpdateDataAsync(int id, PostUpdateModel model)
        {
            throw new NotImplementedException();
        }
    }
}
