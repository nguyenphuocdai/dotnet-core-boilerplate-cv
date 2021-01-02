using System.Collections.Generic;

namespace AspNetCoreHero.Library.Enum
{
    public class PatternEnum
    {
        public const string Date = "yyyyMMdd";
        public const string DateDisplay = "dd/MM/yyyy";
        public const string DateTime = "yyyyMMddHHmmss";
        public const string DateTimeDisplay = "dd/MM/yyyy HH:mm:ss";
        public const string DateTimeUniversal = "yyyy-MM-dd'T'HH:mm:ss'Z'";

        public const string MonthDate = "MMdd";

        public const string FolderCurrent = "./";
        public const string FolderParent = "../";
        public const string GuidDigits = "N";
        public const string Money = "#,##0";
        public const string Number = "#0";
        public const string Currency = "#0.0######";
        public const string CurrencyRate = "#0.#########";
        public const string Time = "HHmmss";
        public const string TimeLog = "HH:mm:ss:fff";

        public const string ASCIINumber = "^[0-9]*$";
        public const string ASCIICharacter = "^[a-zA-Z]*$";
        public const string IPAddress = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";

        public enum DataType
        {
            Alphabet,
            Digit,
            Alphanumeric,
            Name,
            Address,
            Email
        }

        private static readonly Dictionary<DataType, string> RegexDictionary = new Dictionary<DataType, string>
        {
            { DataType.Alphabet, "[^a-zA-Z]" },
            { DataType.Digit, "[^0-9]" },
            { DataType.Alphanumeric, "[^a-zA-Z0-9]" },
            { DataType.Name, "[^a-zA-Z ]|^[ ]+|[ ]+$" },
            { DataType.Address, "[^a-zA-Z0-9 ,./_-]|^[ ]+|[ ]+$" },
            { DataType.Email, "[^a-zA-Z0-9@._-]" }
        };

        public static string GetRegex(DataType dataType)
        {
            return RegexDictionary.ContainsKey(dataType) ? RegexDictionary[dataType] : string.Empty;
        }

    }
}
