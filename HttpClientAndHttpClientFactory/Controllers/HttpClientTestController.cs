using HttpClientAndHttpClientFactory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace HttpClientAndHttpClientFactory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HttpClientTestController : ControllerBase
    {
        private readonly ILogger<HttpClientTestController> _logger;
        private readonly ApiSettings _apiSettings;
        private static readonly HttpClient client = new HttpClient();
        public HttpClientTestController(ILogger<HttpClientTestController> logger, IOptions<ApiSettings> apiSettings)
        {
            _logger = logger;
            _apiSettings = apiSettings.Value;

        }

        [HttpGet(Name = "get-data")]
        public async Task<IActionResult> GetData()
        {
            string apiUrl = $"{_apiSettings.BaseUrl}/posts";
            string apiKey = _apiSettings.ApiKey;

            
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string responseData = await response.Content.ReadAsStringAsync();
                return Ok(responseData);
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Request error: {e.Message}");
            }
        }
    }
}
