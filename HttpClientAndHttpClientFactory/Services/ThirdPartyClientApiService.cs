using HttpClientAndHttpClientFactory.Models;
using HttpClientAndHttpClientFactory.Services.Interface;

namespace HttpClientAndHttpClientFactory.Services
{
    public class ThirdPartyClientApiService : IThirdPartyClientApiService
    {
        private readonly HttpClient _httpClient;

        public ThirdPartyClientApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task DeleteDataAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostModel>> GetDataAsync()
        {
            throw new NotImplementedException();
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
