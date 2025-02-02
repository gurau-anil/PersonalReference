using HttpClientAndHttpClientFactory.Models;

namespace HttpClientAndHttpClientFactory.Services.Interface
{
    public interface IPostService
    {
        Task<IEnumerable<PostModel>> GetDataAsync();
        Task<PostModel> GetDataByIdAsync(int id);
        Task<PostModel> PostDataAsync(PostAddModel model);
        Task<PostModel> UpdateDataAsync(int id, PostUpdateModel model);
        Task<string> DeleteDataAsync(int id);
    }
}
