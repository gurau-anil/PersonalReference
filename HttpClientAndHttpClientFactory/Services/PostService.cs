using HttpClientAndHttpClientFactory.Models;
using Newtonsoft.Json;

namespace HttpClientAndHttpClientFactory.Services.Interface
{
    public class PostService : IPostService
    {
        private readonly IApiService _apiService;
        public PostService(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IEnumerable<PostModel>> GetDataAsync()
        {
            return JsonConvert.DeserializeObject<IEnumerable<PostModel>>(await _apiService.GetDataAsync());
        }
    }
}
