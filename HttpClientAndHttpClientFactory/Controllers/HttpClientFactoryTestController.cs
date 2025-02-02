using HttpClientAndHttpClientFactory.Models;
using HttpClientAndHttpClientFactory.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientAndHttpClientFactory.Controllers
{
    [ApiController]
    [Route("api/approach2")]
    public class HttpClientFactoryTestController : ControllerBase
    {
        private readonly ILogger<HttpClientFactoryTestController> _logger;
        private readonly IPostService _postService;
        public HttpClientFactoryTestController(ILogger<HttpClientFactoryTestController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        [HttpGet]
        [Route("get-data")]
        [Produces(typeof(IEnumerable<PostModel>))]
        public async Task<IActionResult> GetData()
        {

            return await ResponseWrapperAsync(async () =>
            {
                return await _postService.GetDataAsync();
            });
        }

        [HttpGet]
        [Route("get-data/{id:int}")]
        [Produces(typeof(PostModel))]
        public async Task<IActionResult> GetDataById(int id)
        {
            return await ResponseWrapperAsync(async () =>
            {
                return await _postService.GetDataByIdAsync(id);
            });
        }

        [HttpPost]
        [Route("post-data")]
        [Produces(typeof(PostModel))]
        public async Task<IActionResult> PostData([FromBody] PostAddModel model)
        {
            return await ResponseWrapperAsync(async () =>
            {
                return await _postService.PostDataAsync(model);
            });
        }

        [HttpPut]
        [Route("update-data/{id:int}")]
        [Produces(typeof(PostModel))]
        public async Task<IActionResult> UpdateData(int id, [FromBody] PostUpdateModel model)
        {
            return await ResponseWrapperAsync(async () =>
            {
                return await _postService.UpdateDataAsync(id, model);
            });
        }

        [HttpDelete]
        [Route("delete-data/{id:int}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            return await ResponseWrapperAsync(async () =>
            {
                return await _postService.DeleteDataAsync(id);
            });
        }


        private async Task<IActionResult> ResponseWrapperAsync(Func<Task<object>> func)
        {
            try
            {
                return Ok(await func.Invoke());
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Request error: {e.Message}");
            }
        }
    }
}
