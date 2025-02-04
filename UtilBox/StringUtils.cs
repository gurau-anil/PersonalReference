
using System.Text.RegularExpressions;

namespace UtilBox.StringUtils
{
    public static class StringHelper
    {
        public static string GenerateRandomString(int length, bool includeDigits = true, bool includeSpecialChars = false)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            string chars = letters;
            if (includeDigits) chars += digits;
            if (includeSpecialChars) chars += specialChars;

            Random rand = new Random();
            return new string(Enumerable.Range(0, length).Select(_ => chars[rand.Next(chars.Length)]).ToArray());
        }

    }


    public static class StringExtension
    {
        public static int WordCount(this string input)
        {
            var matches = Regex.Matches(input, @"\S+");
            return matches.Count;
        }

        public static string RemoveWhiteSpaces(this string input)
        {
            return Regex.Replace(input, @"\s+", " ");
        }

        public static string CapitalizeFirstLetter(this string input)
        {
            return string.IsNullOrEmpty(input) ? input : char.ToUpper(input[0]) + input.Substring(1);
        }

        public static string ToSentenceCase(this string input)
        {
            return string.IsNullOrEmpty(input) ? input : input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
        }

        public static bool CompareWith(this string input, string compareWith, bool ignoreCase = false)
        {
            return ignoreCase ? string.Equals(input, compareWith, StringComparison.OrdinalIgnoreCase) : string.Equals(input, compareWith);
        }

    }
}
