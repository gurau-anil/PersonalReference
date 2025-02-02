using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
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
            _apiService.apiUrl = "posts";
        }

        public async Task<IEnumerable<PostModel>> GetDataAsync()
        {
            return JsonConvert.DeserializeObject<IEnumerable<PostModel>>(await _apiService.GetDataAsync());
        }

        public async Task<PostModel> GetDataByIdAsync(int id)
        {
            _apiService.apiUrl = $"posts/{id}";
            return JsonConvert.DeserializeObject<PostModel>(await _apiService.GetDataByIdAsync(id));

        }

        public async Task<PostModel> PostDataAsync(PostAddModel model)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            return JsonConvert.DeserializeObject<PostModel>(await _apiService.PostDataAsync(content));
        }

        public async Task<PostModel> UpdateDataAsync(int id, PostUpdateModel model)
        {
            _apiService.apiUrl = $"posts/{id}";
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            return JsonConvert.DeserializeObject<PostModel>(await _apiService.UpdateDataAsync(id, content));
        }

        public async Task<string> DeleteDataAsync(int id)
        {
            _apiService.apiUrl = $"posts/{id}";
            return await _apiService.DeleteDataAsync(id);
        }
    }
}
