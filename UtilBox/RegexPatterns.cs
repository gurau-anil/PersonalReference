using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilBox
{
    public static class RegexPatterns
    {
        public const string Email = @"^(?![.-])([a-zA-Z0-9._%+-]+)@([a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$";
        public const string PhoneNumber = @"^(\+?\d{1,2})?[\s\-]?\(?\d{1,4}[\s\)\-]?\d{1,4}[\s\-]?\d{1,4}$";
        public const string SSN = @"^(?!000|666|9\d{2})\d{3}-(?!00)\d{2}-(?!0000)\d{4}$";
        public const string CreditCard = @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9]{2})[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|7[0-9]{15})$";
        public const string CreditCardExpirationDate = @"^(0[1-9]|1[0-2])\/\d{2}$";
        public const string Url = @"^(https?|ftp):\/\/([a-z0-9-]+\.)+[a-z]{2,6}(:[0-9]{1,5})?(\/[a-zA-Z0-9\-_\/\?&=%#\.]*)?$";
        public const string ImageExtension = @"\.(jpg|jpeg|png|gif|bmp|tiff|webp|svg|ico|heif|heic)$";
        public const string IPv4Address = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        public const string IPv6Address = @"^([0-9a-fA-F]{1,4}:){7}([0-9a-fA-F]{1,4})$";
        public const string USZipCode = @"^\d{5}(-\d{4})?$";
        public const string Alphanumeric = @"^[a-zA-Z0-9]+$";
        public const string StrongPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
        public const string Base64 = @"^(?:[A-Za-z0-9+\/]{4})*(?:[A-Za-z0-9+\/]{2}==|[A-Za-z0-9+\/]{3}=)?$";
        public const string LettersOnly = @"^[a-zA-Z]+$";
        public const string DigitsOnly = @"^\d+$";
        public const string AlphaNumericWithSpecialChars = @"^[a-zA-Z0-9\s\-_@]+$";
        public const string LatitudeLongitude = @"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?)\s*,\s*[-+]?((1[0-7]\d(\.\d+)?)|180(\.0+)?|\d(\.\d+)?)$";
        public const string CurrencyAmount = @"^\$?(\d{1,3})(,\d{3})*(\.\d{2})?$";
        public const string Guid = @"^[{(]?[0-9A-Fa-f]{8}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{12}[)}]?$";
        public const string Numeric = @"^\d+$";
        public const string Alpha = @"^[a-zA-Z]+$";
        public const string AlphaNumeric = @"^[a-zA-Z0-9]+$";
    }
}
