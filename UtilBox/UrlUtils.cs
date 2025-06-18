namespace UtilBox
{
    public enum UrlPart
    {
        Scheme,
        Host,
        Path, 
        Fragment
    }
    public static class UrlUtils
    {

        public static string GetUrlPart(string urlInput, UrlPart part)
        {
            if (!IsValidateUrl(urlInput))
                return string.Empty;

            Uri uri = new Uri(urlInput);
            switch (part)
            {
                case UrlPart.Scheme:
                    return uri.Scheme;
                case UrlPart.Host:
                    return uri.Host;
                case UrlPart.Path:
                    return uri.AbsolutePath;
                case UrlPart.Fragment:
                    return uri.Fragment;
                default:
                    return String.Empty;
            }
        }

        public static bool IsValidateUrl(string urlString)
        {
            return Uri.TryCreate(urlString, UriKind.Absolute, out Uri? uriResult)
                            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps || 
                            uriResult.Scheme == Uri.UriSchemeFtp || uriResult.Scheme == Uri.UriSchemeFtps)
                            && uriResult.Host.Contains('.')
                            && !uriResult.Host.EndsWith(".");
        }

        public static bool IsHttps(string urlString)    
        {
            if (!IsValidateUrl(urlString))
                return false;
            Uri uri = new Uri(urlString);
            return uri.Scheme == Uri.UriSchemeHttps;
        }

        public static bool IsSameDomain(string url1, string url2)
        {
            if (!(IsValidateUrl(url1) && IsValidateUrl(url2)))
                return false;

            return new Uri(url1).Host.Replace("www.", "", StringComparison.OrdinalIgnoreCase)
                .Equals(new Uri(url2).Host.Replace("www.", "", StringComparison.OrdinalIgnoreCase));
        }
    }
}
