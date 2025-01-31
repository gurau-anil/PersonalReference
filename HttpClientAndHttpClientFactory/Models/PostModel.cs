namespace HttpClientAndHttpClientFactory.Models
{
    public class BaseModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
    }

    public class PostModel : BaseModel
    {
        public int Id { get; set; }
    }

    public class PostAddModel : BaseModel
    {
    }
}
