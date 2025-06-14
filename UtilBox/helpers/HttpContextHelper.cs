
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace UtilBox.helpers
{
    public class HttpContextHelper
    {
        public static string? GetRequestPath(IHttpContextAccessor accessor) => accessor.HttpContext?.Request?.Path.Value;
        public static string? GetHeader(IHttpContextAccessor accessor, string headerName) => accessor.HttpContext?.Request?.Headers[headerName];
        public static IHeaderDictionary? GetHeaders(IHttpContextAccessor accessor) => accessor.HttpContext?.Request?.Headers;
        public static string GetFullUrl(IHttpContextAccessor accessor) => $"{accessor.HttpContext?.Request?.Scheme}://{accessor.HttpContext?.Request?.Host.Value}";
        public static string? GetHttpMethod(IHttpContextAccessor accessor) => accessor.HttpContext?.Request?.Method;



        public static string? GetClientIp(IHttpContextAccessor accessor) => accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        public static int GetClientPort(IHttpContextAccessor accessor) => accessor.HttpContext?.Connection?.RemotePort?? default;
        public static string? GetClientConnetionId(IHttpContextAccessor accessor) => accessor.HttpContext?.Connection?.Id;




        public static void SetItem(IHttpContextAccessor accessor, string key, object value) => accessor.HttpContext?.Items.TryAdd(key, value);
        public static object? GetItem(IHttpContextAccessor accessor, string key) => accessor.HttpContext?.Items.TryGetValue(key, out var value) == true ? value : null;




        public static string? GetUserId(IHttpContextAccessor accessor) => accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public static string? GetUsername(IHttpContextAccessor accessor) => accessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
        public static string? GetEmail(IHttpContextAccessor accessor) => accessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        public static bool IsAuthenticated(IHttpContextAccessor accessor) => accessor.HttpContext?.User?.Identity?.IsAuthenticated == true;
        public static bool IsInRole(IHttpContextAccessor accessor, string role) => accessor.HttpContext?.User?.IsInRole(role) == true;
        public static List<string> GetRoles(IHttpContextAccessor accessor) => accessor.HttpContext?.User?.FindAll(ClaimTypes.Role)
                .Select(r => r.Value)
                .ToList() ?? new List<string>();
        public static Dictionary<string, string> GetAllClaims(IHttpContextAccessor accessor) => accessor.HttpContext?.User?.Claims
            .ToDictionary(c => c.Type, c => c.Value) ?? new Dictionary<string, string>();
        public static string? GetClaim(IHttpContextAccessor accessor, string claimType) => accessor.HttpContext?.User?.FindFirst(claimType)?.Value;
        public static IQueryCollection? GetQueryParams(IHttpContextAccessor accessor) => accessor.HttpContext?.Request?.Query;
        public static string? GetQueryParam(IHttpContextAccessor accessor, string key) => accessor.HttpContext?.Request?.Query[key];





        public static string? GetCookie(IHttpContextAccessor accessor, string key) => accessor.HttpContext?.Request?.Cookies[key];
        public static void SetCookie(IHttpContextAccessor accessor, string key, string value, int? expireDays = null)
        {
            CookieOptions cookieOptions = new CookieOptions();
            if (expireDays.HasValue)
                cookieOptions.Expires = DateTime.UtcNow.AddDays(expireDays.Value);

            accessor.HttpContext?.Response.Cookies.Append(key, value, cookieOptions);
        }




        public static void SetStatusCode(IHttpContextAccessor accessor, int statusCode)
        {
            if (accessor.HttpContext != null)
                accessor.HttpContext.Response.StatusCode = statusCode;
        }


        public static void SetResponseHeader(IHttpContextAccessor accessor, string key, string value)
        {
            if (accessor.HttpContext != null)
                accessor.HttpContext.Response.Headers[key] = value;
        }


        public static async Task WriteResponseAsync(IHttpContextAccessor accessor, string content, string contentType = "text/plain", int? statusCode = null)
        {
            var response = accessor.HttpContext?.Response;
            if (response == null) return;

            response.ContentType = contentType;
            if (statusCode.HasValue)
                response.StatusCode = statusCode.Value;

            await response.WriteAsync(content);
        }


    }
}
