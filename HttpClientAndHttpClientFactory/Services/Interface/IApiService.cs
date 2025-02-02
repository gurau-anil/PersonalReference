using HttpClientAndHttpClientFactory.Models;

namespace HttpClientAndHttpClientFactory.Services.Interface
{
    public interface IApiService
    {
        Task<string> GetDataAsync();
        Task<PostModel> GetDataByIdAsync(int id);
        Task<PostModel> PostDataAsync(PostAddModel model);
        Task PostFileDataAsync(IFormFile file);
        Task<PostModel> UpdateDataAsync(int id, PostUpdateModel model);
        Task DeleteDataAsync(int id);
    }
}
