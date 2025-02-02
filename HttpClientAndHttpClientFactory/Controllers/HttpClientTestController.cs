using System.Net.Http.Headers;
using System.Text;
using HttpClientAndHttpClientFactory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HttpClientAndHttpClientFactory.Controllers
{
    [ApiController]
    [Route("api/approach1")]
    public class HttpClientTestController : ControllerBase
    {
        private readonly ILogger<HttpClientTestController> _logger;
        private readonly ApiSettings _apiSettings;

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

        [HttpGet]
        [Route("get-data")]
        [Produces(typeof(IEnumerable<PostModel>))]
        public async Task<IActionResult> GetData()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    PrepareHttpClient(client);

                    //this custom header is specific to this request
                    client.DefaultRequestHeaders.Add("Custom-Header", "value");


                    //instead of passing full address like this, we can set Base address and pass only the remaining path
                    //HttpResponseMessage response = await client.GetAsync(apiUrl);

                    HttpResponseMessage response = await client.GetAsync("posts");
                    response.EnsureSuccessStatusCode();

                    //Reading Raw Response Data
                    string responseData = await response.Content.ReadAsStringAsync();

                    return Ok(JsonConvert.DeserializeObject<IEnumerable<PostModel>>(responseData));
                }

            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Request error: {e.Message}");
            }
        }

        [HttpGet]
        [Route("get-data/{id:int}")]
        [Produces(typeof(PostModel))]
        public async Task<IActionResult> GetDataById(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    PrepareHttpClient(client);

                    //this custom header is specific to this request
                    client.DefaultRequestHeaders.Add("Custom-Header", "value");

                    HttpResponseMessage response = await client.GetAsync($"posts/{id}");
                    response.EnsureSuccessStatusCode();

                    //Reading Raw Response Data
                    string responseData = await response.Content.ReadAsStringAsync();

                    return Ok(JsonConvert.DeserializeObject<PostModel>(responseData));
                }

            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Request error: {e.Message}");
            }
        }

        [HttpPost]
        [Route("post-data")]
        [Produces(typeof(PostModel))]
        public async Task<IActionResult> PostData([FromBody] PostAddModel model)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    PrepareHttpClient(client);

                    StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("posts", jsonContent);
                    response.EnsureSuccessStatusCode();

                    string responseData = await response.Content.ReadAsStringAsync();

                    return Ok(JsonConvert.DeserializeObject<PostModel>(responseData));
                }
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Request error: {e.Message}");
            }
        }

        [HttpPost]
        [Route("post-file-data")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostFileData(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                MultipartFormDataContent content = new MultipartFormDataContent();

                // Add the file to form data
                StreamContent fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                content.Add(fileContent, "file", file.FileName);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://www.postb.in");

                    HttpResponseMessage response = await client.PostAsync("1738363049419-8408013498410", content);
                    return response.IsSuccessStatusCode? Ok(new { Message = "File uploaded successfully" }) : StatusCode((int)response.StatusCode, "Failed to upload file.");
                }
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Request error: {e.Message}");
            }
        }

        [HttpPut]
        [Route("update-data/{id:int}")]
        [Produces(typeof(PostModel))]
        public async Task<IActionResult> UpdateData(int id, [FromBody] PostUpdateModel model)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    PrepareHttpClient(client);

                    StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    //This is just a mock, first we need to check if the post with provided id exists.
                    HttpResponseMessage response = await client.PutAsync($"posts/{id}", jsonContent);
                    response.EnsureSuccessStatusCode();

                    string responseData = await response.Content.ReadAsStringAsync();

                    return Ok(JsonConvert.DeserializeObject<PostModel>(responseData));
                }
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Request error: {e.Message}");
            }
        }

        [HttpDelete]
        [Route("delete-data/{id:int}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    PrepareHttpClient(client);

                    HttpResponseMessage response = await client.DeleteAsync($"posts/{id}");
                    response.EnsureSuccessStatusCode();

                    return Ok();
                }
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, $"Request error: {e.Message}");
            }
        }


        private void PrepareHttpClient(HttpClient client)
        {
            client.Timeout = TimeSpan.FromSeconds(30); // Sets a 30-second timeout
            client.BaseAddress = new Uri(_apiSettings.BaseUrl);
            AddRequiredHeaders(client);
        }

        private void AddRequiredHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
