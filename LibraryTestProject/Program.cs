using UtilBox.StringUtils;

internal class Program
{
    private static void Main(string[] args)
    {
        string name = "Anil gurau";

        string anotherName = "anil gurau";

        string phoneNumber = "5205263540";
        string cardNumber = "3450 2356 2356 4598";
        Console.WriteLine(StringHelper.GenerateRandomPassword(10));
        Console.WriteLine(StringHelper.GenerateRandomString(10));
        Console.WriteLine(StringHelper.GenerateUniqueIdentifier());
        Console.WriteLine(phoneNumber.FormatAndMaskPhoneNumber());
        Console.WriteLine(cardNumber.MaskCreditCard());
        Console.WriteLine(name.CompareWith(anotherName, ignoreCase: true));
    }
}