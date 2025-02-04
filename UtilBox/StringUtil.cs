
using System.Text.RegularExpressions;

namespace UtilBox
{
    public class StringUtil
    {
        public string GenerateRandomString(int length, bool includeDigits = true, bool includeSpecialChars = false)
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

        public int WordCount(string input)
        {
            var matches = Regex.Matches(input, @"\S+");
            return matches.Count;
        }

        public string RemoveWhiteSpaces(string input)
        {
            return Regex.Replace(input, @"\s+", " ");
        }

        public static string CapitalizeFirstLetter(string input)
        {
            return string.IsNullOrEmpty(input)? input : char.ToUpper(input[0]) + input.Substring(1);
        }

        public string ToSentenceCase(string input)
        {
            return string.IsNullOrEmpty(input) ? input : input.Substring(0, 1).ToUpper() + input.Substring(1).ToLower();
        }


    }


}
