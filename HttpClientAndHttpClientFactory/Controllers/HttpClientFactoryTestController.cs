using System;
using System.Net.Http.Headers;
using System.Text;
using HttpClientAndHttpClientFactory.Models;
using HttpClientAndHttpClientFactory.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HttpClientAndHttpClientFactory.Controllers
{
    [ApiController]
    [Route("api/approach2")]
    public class HttpClientFactoryTestController : ControllerBase
    {
        private readonly ILogger<HttpClientFactoryTestController> _logger;
        private readonly IPostService _postService;
        private readonly ApiSettings _apiSettings;

        private readonly string apiUrl;
        private readonly string apiKey;
        public HttpClientFactoryTestController(ILogger<HttpClientFactoryTestController> logger, IOptions<ApiSettings> apiSettings, IPostService postService)
        {
            _logger = logger;
            _apiSettings = apiSettings.Value;


            //Just a Mock Api
            apiUrl = $"{_apiSettings.BaseUrl}/posts";
            apiKey = _apiSettings.ApiKey;
            _postService = postService;
        }

        [HttpGet]
        [Route("get-data")]
        [Produces(typeof(IEnumerable<PostModel>))]
        public async Task<IActionResult> GetData()
        {
            try
            {
                return Ok(await _postService.GetDataAsync());
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Request error: {e.Message}");
            }
        }

        
    }
}
