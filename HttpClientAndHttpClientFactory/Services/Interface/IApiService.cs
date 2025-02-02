using HttpClientAndHttpClientFactory.Models;

namespace HttpClientAndHttpClientFactory.Services.Interface
{
    public interface IApiService
    {
        public string apiUrl { get; set; }
        Task<string> GetDataAsync();
        Task<string> GetDataByIdAsync(int id);
        Task<string> PostDataAsync(HttpContent content);
        Task<string> UpdateDataAsync(int id, HttpContent content);
        Task<string> DeleteDataAsync(int id);
    }
}
