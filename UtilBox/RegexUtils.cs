using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UtilBox
{
    public static class RegexUtils
    {
        public static bool IsMatch(string input, string pattern, RegexOptions options = RegexOptions.None)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        public static Match Match(string input, string pattern, RegexOptions options = RegexOptions.None)
        {
            return Regex.Match(input, pattern, options);
        }

        public static MatchCollection Matches(string input, string pattern, RegexOptions options = RegexOptions.None)
        {
            return Regex.Matches(input, pattern, options);
        }

        public static string Replace(string input, string pattern, string replacement, RegexOptions options = RegexOptions.None)
        {
            return Regex.Replace(input, pattern, replacement, options);
        }

        public static string Replace(string input, string pattern, MatchEvaluator evaluator, RegexOptions options = RegexOptions.None)
        {
            return Regex.Replace(input, pattern, evaluator, options);
        }
    }
}
