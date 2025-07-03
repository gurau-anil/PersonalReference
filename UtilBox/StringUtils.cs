
using System.Text;
using System.Text.RegularExpressions;

namespace UtilBox.StringUtils
{
    public static class StringHelper
    {
        public static string GenerateRandomString(int length, bool includeDigits = true, bool includeSpecialChars = false)
        {
            string retVal = string.Empty;

            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            string validCharacters = letters;
            if (includeDigits) validCharacters += digits;
            if (includeSpecialChars) validCharacters += specialChars;

            Random rand = new Random();

            for (int i = 0; i < length; i++)
            {
                retVal += validCharacters[rand.Next(validCharacters.Length)];
            }

            return retVal;
        }

        public static string GenerateRandomPassword(int length)
        {
            const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            Random random = new Random();

            StringBuilder password = new StringBuilder();

            //appending random character from UpperCase String
            password.Append(upperCase[random.Next(upperCase.Length)]);

            // appending random character from lowercase String
            password.Append(lowerCase[random.Next(lowerCase.Length)]);

            // appending random character from digits String
            password.Append(digits[random.Next(digits.Length)]);

            // appending random character from special characters String
            password.Append(specialChars[random.Next(specialChars.Length)]);

            //adding remaining characters of the password
            const string allValidChars = upperCase + lowerCase + digits + specialChars;
            for (int i = password.Length; i < length; i++)
            {
                password.Append(allValidChars[random.Next(allValidChars.Length)]);
            }

            // Shuffling the password to randomize
            return new string(password.ToString().OrderBy(c => random.Next()).ToArray());
        }

        public static string GenerateUniqueIdentifier()
        {
            return Guid.NewGuid().ToString();
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

        public static string RemoveSpecialCharacters(this string input)
        {
            // removes characters other than alpabets, numbers and space
            return Regex.Replace(input, "[^a-zA-Z0-9 ]", "");
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

        public static string ConvertToBase64(this string input)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(byteArray);
        }

        public static string ConvertFromBase64(this string base64Encoded)
        {
            byte[] byteArray = Convert.FromBase64String(base64Encoded);
            return Encoding.UTF8.GetString(byteArray);
        }

        #region formatting
        public static string Mask(this string input, char maskCharacter='x')
        {
            if (string.IsNullOrEmpty(input) || input.Length < 4)
                return input;

            // Mask all but the last 4 digits
            return new string(maskCharacter, input.Length - 4) + input.Substring(input.Length - 4);
        }

        public static string FormatPhoneNumber(this string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace("-","");
            StringBuilder builder = new StringBuilder("");
            builder.Append(phoneNumber.Substring(0,3));
            builder.Append("-");
            builder.Append(phoneNumber.Substring(3,3));
            builder.Append("-");
            builder.Append(phoneNumber.Substring(6));
            return builder.ToString();
        }

        
        public static string MaskPhoneNumber(this string phoneNumber)
        {
            return phoneNumber.maskExceptLast(4);
        }

        public static string MaskCreditCard(this string cardNumber)
        {
            return cardNumber.maskExceptLast(4);
        }

        public static string MaskSSN(this string ssn)
        {
            return ssn.maskExceptLast(4);
        }

        public static string FormatAndMaskPhoneNumber(this string phoneNumber)
        {
            return MaskPhoneNumber(phoneNumber.FormatPhoneNumber());
        }

        #endregion

        public static string TruncateWithEllipsis(this string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input) || maxLength <= 3)
                return input;

            return input.Length > maxLength ? input.Substring(0, maxLength - 3) + "..." : input;
        }

        #region private
        private static string maskExceptLast(this string input, int length)
        {
            int index = input.Length - length;
            string lastChars = input.Substring(index);
            return Regex.Replace(input.Substring(0, index), @"\d", "x") + lastChars;
        }

        #endregion
    }
}
