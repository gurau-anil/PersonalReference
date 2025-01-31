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

        private readonly string apiUrl;
        private readonly string apiKey;
        public HttpClientTestController(ILogger<HttpClientTestController> logger, IOptions<ApiSettings> apiSettings)
        {
            _logger = logger;
            _apiSettings = apiSettings.Value;


            //Just a Mock Api
            apiUrl = $"{_apiSettings.BaseUrl}/posts";
            apiKey = _apiSettings.ApiKey;
        }

        [HttpGet(Name = "get-data")]
        public async Task<IActionResult> GetData()
        {

            PrepareHttpClient(client);

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                //Reading Raw Response Data
                string responseData = await response.Content.ReadAsStringAsync();
                return Ok(responseData);
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Request error: {e.Message}");
            }
        }

        private void PrepareHttpClient(HttpClient client)
        {
            AddRequestHeaders(client);
        }

        private void AddRequestHeaders(HttpClient client)
        {
            //adding custom Headers
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }
    }
}
