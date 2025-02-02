using HttpClientAndHttpClientFactory.Models;

namespace HttpClientAndHttpClientFactory.Services.Interface
{
    public interface IPostService
    {
        Task<IEnumerable<PostModel>> GetDataAsync();
    }
}
