using AspNetCoreHero.Library.Enum;
using AspNetCoreHero.Library.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace AspNetCoreHero.Library.Global
{
    public static class FunctionBase
    {
        /// <summary>
        ///     Create new folder if it is not exist
        /// </summary>
        public static void CreateFolder(string folderPath)
        {
            if (Directory.Exists(folderPath) == false)
            {
                Directory.CreateDirectory(folderPath);
            }
        }


        /// <summary>
        ///     Convert Object to Dictionary
        /// </summary>
        public static Dictionary<string, string> ConvertToDictionary<T>(T data) where T : class
        {
            return data
                .GetType()
                .GetProperties()
                .ToDictionary(
                    property => property.Name,
                    property => property.GetValue(data) + string.Empty);
        }


        /// <summary>
        ///     Convert DataTable to Dictionary
        /// </summary>
        public static Dictionary<string, string> ConvertToDictionary(DataRow row)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (DataColumn column in row.Table.Columns)
            {
                dictionary.Add(column.ColumnName, ToString(row[column]));
            }

            return dictionary;
        }


        /// <summary>
        ///     Convert folder path to general format.
        ///     Use slash instead of backslash and be sure it must end with slash character.
        /// </summary>
        public static string ConvertToPathFormat(string value)
        {
            List<string> list = new List<string> { CharacterEnum.Backslash, CharacterEnum.Slash };
            value += list.Contains(string.Empty + value[value.Length - 1]) ? string.Empty : CharacterEnum.Slash;
            return value
                .Replace(CharacterEnum.Backslash, CharacterEnum.Slash)
                .Replace(PatternEnum.FolderCurrent, string.Empty);
        }


        /// <summary>
        ///     Convert string to integer
        /// </summary>
        public static int ConvertToInteger(string value, int defaultValues = 0)
        {
            if (int.TryParse(value, out int result) == false)
            {
                result = defaultValues;
            }

            return result;
        }


        /// <summary>
        ///     Convert string data into InsensitiveDictionary
        /// </summary>
        public static InsensitiveDictionary<string> Deserialize(string data, string contentType = ContentEnum.JSON)
        {
            switch (contentType)
            {
                case ContentEnum.JSON:
                    return Deserialize<InsensitiveDictionary<string>>(data);

                case ContentEnum.XML:
                    return ParseXmlToDictionary<InsensitiveDictionary<string>>(data);

                default:
                    return null;
            }
        }


        /// <summary>
        ///     Convert string data into object
        /// </summary>
        public static T Deserialize<T>(string data, string contentType = ContentEnum.JSON) where T : class
        {
            switch (contentType)
            {
                case ContentEnum.JSON:
                    return JsonSerializer.Deserialize(data, typeof(T)) as T;

                case ContentEnum.XML:
                    //return XmlSerializer.DeserializeFromString(data, typeof(T)) as T;

                default:
                    return null;
            }
        }


        /// <summary>
        ///     Extract characters starting at the left side of text
        /// </summary>
        public static string ExtractLeft(string value, int length)
        {
            return value.Substring(0, length);
        }


        /// <summary>
        ///     Extract characters between start and end position
        /// </summary>
        public static string ExtractMiddle(string value, int startIndex, int endIndex)
        {
            return value.Remove(endIndex).Substring(startIndex);
        }


        /// <summary>
        ///     Extract characters starting at the right side of text
        /// </summary>
        public static string ExtractRight(string value, int length)
        {
            return value.Substring(value.Length - length);
        }


        /// <summary>
        ///     Mask card number
        /// </summary>
        public static string MaskCardNumber(string cardNumber)
        {
            return cardNumber.Length < 16
                ? cardNumber
                : $"{ExtractLeft(cardNumber, 6)}{MaskData(cardNumber.Length - 10)}{ExtractRight(cardNumber, 4)}";
        }


        /// <summary>
        ///     Mask sensitive data with *
        /// </summary>
        public static string MaskData(int length)
        {
            return new string('*', length);
        }


        /// <summary>
        ///     Use to mask sensitive fields
        /// </summary>
        public static string MaskSensitiveField(string field, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            switch (field)
            {
                case FieldEnum.CardNumber:
                case FieldEnum.ISOF02:
                    return MaskCardNumber(value);

                case FieldEnum.ISOF35:
                    List<string> listData = value.Split('D', '=').ToList();
                    string cardNumber = FunctionBase.MaskCardNumber(listData[0]);
                    return cardNumber + FunctionBase.MaskData(value.Remove(0, cardNumber.Length).Length);

                default:
                    return MaskData(value.Length);
            }
        }


        /// <summary>
        ///     Convert xml string data into Dictionary
        /// </summary>
        public static T ParseXmlToDictionary<T>(string xml) where T : IDictionary<string, string>
        {
            XDocument document = XDocument.Parse(xml);
            T dictionary = Activator.CreateInstance<T>();
            if (document.Root == null)
            {
                return dictionary;
            }

            foreach (XElement element in document.Root.Elements())
            {
                string keyName = element.Name.LocalName;
                if (dictionary.ContainsKey(keyName) == false)
                {
                    dictionary.Add(keyName, element.Value);
                }
            }

            return dictionary;
        }


        /// <summary>
        ///     Read file from disk
        /// </summary>
        public static string ReadFile(string path)
        {
            if (File.Exists(path) == false)
            {
                return null;
            }

            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }


        /// <summary>
        ///     Read file from stream
        /// </summary>
        public static string ReadFile(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }


        /// <summary>
        ///     Convert object to string data
        /// </summary>
        public static string Serialize<T>(T data, string contentType = ContentEnum.JSON) where T : class
        {
            switch (contentType)
            {
                case ContentEnum.JSON:
                    return JsonSerializer.Serialize(data);

                case ContentEnum.XML:
                    return SerializeToXML(data);

                default:
                    return string.Empty;
            }
        }


        public static string SerializeToXML<T>(T value) where T : class
        {
            string xml;
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (MemoryStream writer = new MemoryStream())
            {
                serializer.Serialize(writer, value);
                writer.Position = 0;
                xml = new StreamReader(writer).ReadToEnd();
            }

            return xml;
        }


        /// <summary>
        ///     Get CDATA string
        /// </summary>
        public static string GetCDataSection(string xml)
        {
            return $"<![CDATA[{xml}]]>";
        }

        /// <summary>
        ///     Get InnerXML of XElement
        /// </summary>
        public static string GetInnerXML(XNode element, bool isIncludeRoot)
        {
            if (isIncludeRoot)
            {
                return element.ToString();
            }

            using (XmlReader reader = element.CreateReader())
            {
                reader.MoveToContent();
                return reader.ReadInnerXml();
            }
        }


        /// <summary>
        ///     Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        public static bool IsNullOrWhiteSpace(params string[] array)
        {
            return array.Any(string.IsNullOrWhiteSpace);
        }


        /// <summary>
        ///     Indicates whether a specified string is in array.
        /// </summary>
        public static bool IsInArray(string value, params string[] array)
        {
            return array.Any(data => value == data);
        }


        /// <summary>
        ///     Indicates whether a specified integer is in array.
        /// </summary>
        public static bool IsInArray(int value, params int[] array)
        {
            return array.Any(data => value == data);
        }


        /// <summary>
        ///     Indicates whether a specified long is in array.
        /// </summary>
        public static bool IsInArray(long value, params long[] parameters)
        {
            return parameters.Any(data => value == data);
        }


        /// <summary>
        ///     Return the first string which is not null, empty or consists only of white-space.
        /// </summary>
        public static string GetCoalesceString(params string[] parameter)
        {
            foreach (string data in parameter)
            {
                if (string.IsNullOrWhiteSpace(data) == false)
                {
                    return data;
                }
            }

            return string.Empty;
        }


        public static decimal GetCoalesceValue(params decimal[] parameter)
        {
            return parameter.FirstOrDefault(value => value > 0);
        }


        /// <summary>
        ///     Return the current Universal date time
        /// </summary>
        public static string GetCurrentUniversalTime()
        {
            return DateTime.UtcNow.ToString(PatternEnum.DateTimeUniversal);
        }


        /// <summary>
        ///     Return the current date time yyyyMMdd
        /// </summary>
        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString(PatternEnum.Date);
        }


        /// <summary>
        ///     Return the current date time yyyyMMddHHmmss
        /// </summary>
        public static string GetCurrentDateTime()
        {
            return DateTime.Now.ToString(PatternEnum.DateTime);
        }

        public static long GetDateTimeModify()
        {
            return long.Parse(GetCurrentDateTime());
        }


        /// <summary>
        ///     Random an GUID
        /// </summary>
        public static string GetGUID(string pattern = PatternEnum.GuidDigits)
        {
            return Guid.NewGuid().ToString(pattern);
        }


        /// <summary>
        ///     Get Assembly GUID
        /// </summary>
        public static string GetGUID(Assembly assembly)
        {
            return assembly?.GetCustomAttribute<GuidAttribute>()?.Value;
        }


        /// <summary>
        ///     Get Class Type GUID
        /// </summary>
        public static string GetGUID(Type type)
        {
            return type?.GUID.ToString(PatternEnum.GuidDigits);
        }


        /// <summary>
        ///     Load Assembly to Bytes
        /// </summary>
        public static Assembly LoadAssembly(string assemblyPath)
        {
            try
            {
                return Assembly.LoadFrom(assemblyPath);
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        ///     Auto bind data from Dictionary to Type
        /// </summary>
        public static T AutoBind<T>(InsensitiveDictionary<string> dictData, Type sourceType) where T : class
        {
            if (dictData == null || dictData.Count == 0)
            {
                return null;
            }

            Type type = typeof(T);
            T instance = (T)Activator.CreateInstance(type);
            foreach (FieldInfo field in sourceType.GetFields())
            {
                string key = field.GetValue(null).ToString();
                type.GetProperty(field.Name)?.SetValue(instance, dictData.GetValue(key));
                type.GetField(field.Name)?.SetValue(instance, dictData.GetValue(key));
            }

            return instance;
        }


        /// <summary>
        ///     Format decimal to display
        /// </summary>
        public static string FormatDecimal(string value)
        {
            return ToDecimal(value, out decimal result)
                ? FormatDecimal(result)
                : value;
        }


        /// <summary>
        ///     Format decimal to display
        /// </summary>
        public static string FormatDecimal(decimal value)
        {
            return value.ToString(PatternEnum.Money, CultureInfo.InvariantCulture);
        }


        /// <summary>
        ///     Indicates whether a string is a number in range
        /// </summary>
        public static bool IsInvalidNumber(string value, decimal min, decimal max)
        {
            return decimal.TryParse(value, out decimal result) == false
                || result < min
                || result > max;
        }


        /// <summary>
        ///     Remove invalid characters follow the pattern
        /// </summary>
        public static string GetASCIIData(
            string value,
            PatternEnum.DataType dataType = PatternEnum.DataType.Alphanumeric,
            bool isUpperCase = true)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            // Strip all unicode characters
            value = RemoveUnicode(value);

            // Strip all special characters
            string regex = PatternEnum.GetRegex(dataType);
            if (string.IsNullOrWhiteSpace(regex) == false)
            {
                value = Regex.Replace(value, regex, string.Empty);
            }

            // Strip all redundancy space characters
            if (dataType == PatternEnum.DataType.Name || dataType == PatternEnum.DataType.Address)
            {
                value = Regex.Replace(value, "[ ]{2,}", " ");
            }

            return isUpperCase ? value.ToUpper() : value;
        }


        /// <summary>
        ///     Remove unicode characters
        /// </summary>
        public static string RemoveUnicode(string value)
        {
            string normalizedString = value.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            value = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            return value.Replace('\u0111', 'd').Replace('\u0110', 'D');
        }


        /// <summary>
        ///     Random a number
        /// </summary>
        public static string GetRandomNumber(int length)
        {
            string value = $"1{new string('0', length)}";
            if (int.TryParse(value, out int maxValue) == false)
            {
                maxValue = int.MaxValue;
            }

            Random random = new Random();
            int randNum = random.Next(maxValue);
            return randNum.ToString($"D{length}");
        }


        /// <summary>
        ///     Indicates a integer value is in range
        /// </summary>
        public static bool IsInRange(int value, int min, int max)
        {
            return value >= min && value <= max;
        }


        /// <summary>
        ///     Format Date to Display
        /// </summary>
        public static string FormatDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            bool isDateFormat = value.Length == PatternEnum.Date.Length;
            return DateTime.TryParseExact(value, isDateFormat ? PatternEnum.Date : PatternEnum.DateTime,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date)
                ? date.ToString(isDateFormat ? PatternEnum.DateDisplay : PatternEnum.DateTimeDisplay)
                : string.Empty;
        }


        /// <summary>
        ///     Convert to second
        /// </summary>
        public static int ConvertToSecond(int value, int defaultValue)
        {
            if (value <= 0)
            {
                value = defaultValue;
            }

            return value < UnitEnum.Second
                ? value * UnitEnum.Second
                : value;
        }


        /// <summary>
        ///     Convert string to Base64
        /// </summary>
        public static string ConvertToBase64String(string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }


        /// <summary>
        ///     Convert string to Base32
        /// </summary>
        public static string ConvertToBase32String(string value)
        {
            const int inByteSize = 8;
            const int outByteSize = 5;
            byte[] data = Encoding.UTF8.GetBytes(value);
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();
            StringBuilder result = new StringBuilder((data.Length + 7) * inByteSize / outByteSize);

            int i = 0, index = 0;
            while (i < data.Length)
            {
                byte currentByte = data[i];

                /* Is the current digit going to span a byte boundary? */
                int digit;
                if (index > inByteSize - outByteSize)
                {
                    int nextByte = i + 1 < data.Length ? data[i + 1] : 0;
                    digit = currentByte & (0xFF >> index);
                    index = (index + outByteSize) % inByteSize;
                    digit <<= index;
                    digit |= nextByte >> (inByteSize - index);
                    i++;
                }
                else
                {
                    digit = (currentByte >> (inByteSize - (index + outByteSize))) & 0x1F;
                    index = (index + outByteSize) % inByteSize;
                    if (index == 0)
                    {
                        i++;
                    }
                }

                result.Append(alphabet[digit]);
            }

            return result.ToString();
        }


        /// <summary>
        ///     Insert space at index
        /// </summary>
        public static string InsertSpace(string value, int length)
        {
            return string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : Regex.Replace(value, $".{{{length}}}", "$0 ");
        }


        /// <summary>
        ///     Try deserialize a string to object
        /// </summary>
        public static bool TryDeserialize<T>(
            string data,
            out T value,
            string contentType = ContentEnum.JSON)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                value = null;
                return false;
            }

            value = Deserialize<T>(data, contentType);
            return value != null;
        }


        /// <summary>
        ///     Clone an object
        /// </summary>
        public static T Clone<T>(object data) where T : class
        {
            return Deserialize<T>(Serialize(data));
        }


        public static IList Cast(IList listItems, Type type)
        {
            Type listType = typeof(List<>).MakeGenericType(type);
            IList listResult = (IList)Activator.CreateInstance(listType);

            foreach (object item in listItems)
            {
                listResult.Add(item);
            }

            return listResult;
        }


        public static List<T> Cast<T>(DataTable table)
        {
            List<T> list = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                list.Add(Cast<T>(row));
            }

            return list;
        }


        public static T Cast<T>(DataRow row)
        {
            Type type = typeof(T);
            T instance = (T)Activator.CreateInstance(type);

            foreach (DataColumn column in row.Table.Columns)
            {
                string fieldName = column.ColumnName;
                object fieldValue = row[fieldName];
                Type fieldType = fieldValue.GetType();

                // Set Property Value
                PropertyInfo propertyInfo = type.GetProperty(fieldName);
                if (propertyInfo != null)
                {
                    if (TryConvert(fieldType, propertyInfo.PropertyType, ref fieldValue))
                    {
                        propertyInfo.SetValue(instance, fieldValue);
                    }

                    continue;
                }

                // Set Field Value
                FieldInfo fieldInfo = type.GetField(fieldName);
                if (fieldInfo != null
                    && TryConvert(fieldType, fieldInfo.FieldType, ref fieldValue))
                {
                    fieldInfo.SetValue(instance, fieldValue);
                }
            }

            return instance;
        }


        private static bool TryConvert(Type source, Type target, ref object value)
        {
            try
            {
                if (source != target)
                {
                    value = Convert.ChangeType(value, target, CultureInfo.InvariantCulture);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        public static T1 TryConvert<T1, T2>(T2 value)
            where T1 : class
            where T2 : class
        {
            Type sourceType = typeof(T2);
            Type destinationType = typeof(T1);
            T1 instance = (T1)Activator.CreateInstance(destinationType);

            foreach (FieldInfo sourceFieldInfo in sourceType.GetFields())
            {
                FieldInfo destinationFieldInfo = destinationType.GetField(sourceFieldInfo.Name);
                if (destinationFieldInfo == null)
                {
                    continue;
                }

                object fieldValue = sourceFieldInfo.GetValue(value);
                if (TryConvert(sourceFieldInfo.FieldType, destinationFieldInfo.FieldType, ref fieldValue))
                {
                    destinationFieldInfo.SetValue(instance, fieldValue);
                }
            }

            foreach (PropertyInfo sourcePropertyInfo in sourceType.GetProperties())
            {
                PropertyInfo destinationPropertyInfo = destinationType.GetProperty(sourcePropertyInfo.Name);
                if (destinationPropertyInfo == null)
                {
                    continue;
                }

                object propertyValue = sourcePropertyInfo.GetValue(value);
                if (TryConvert(sourcePropertyInfo.PropertyType, destinationPropertyInfo.PropertyType, ref propertyValue))
                {
                    destinationPropertyInfo.SetValue(instance, propertyValue);
                }
            }

            return instance;
        }


        public static decimal ToDecimal(string value)
        {
            ToDecimal(value, out decimal result);
            return result;
        }


        public static bool ToDecimal(string value, out decimal result)
        {
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }


        public static string ToString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            Type type = value.GetType();
            if (type == typeof(decimal))
            {
                return ToString((decimal)value);
            }

            if (type == typeof(bool))
            {
                return ToString((bool)value);
            }

            return value + string.Empty;
        }


        public static string ToString(decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }


        public static string ToString(bool value)
        {
            return value ? bool.TrueString : bool.FalseString;
        }


        public static string EscapeJSON(string value)
        {
            return value
                .Replace(Environment.NewLine, "\\r\\n")
                .Replace("\"", "\\\"");
        }


        public static MemoryStream SerializeToStream(object data)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            return stream;
        }


        public static T DeserializeFromStream<T>(MemoryStream stream) where T : class
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            T value = formatter.Deserialize(stream) as T;
            return value;
        }

        public static bool IsMatch(string value, string pattern)
        {
            return new Regex(pattern).IsMatch(value);
        }

        public static bool IsIPAddress(string value)
        {
            return IsMatch(value, PatternEnum.IPAddress);
        }

        public static bool IsInvalidPhoneNumber(string value)
        {
            return Regex.Match(value, @"^(0[1-9][0-9]{8})$").Success == false;
        }

        /// <summary>
        /// Gets the currency amount base on currency code.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="currencyCode">The currency code.</param>
        /// <returns>
        ///     the currency amount as decimal
        /// </returns>
        public static decimal GetCurrencyAmount(string amount, string currencyCode = CurrencyEnum.VND)
        {
            if (string.IsNullOrWhiteSpace(amount) ||
                amount.Length < 2 ||
                decimal.TryParse(amount, out decimal _) == false)
            {
                return 0;
            }

            switch (currencyCode)
            {
                case CurrencyEnum.VND:
                    amount = amount.Remove(amount.Length - 2);
                    return decimal.Parse(amount);

                default:
                    return decimal.Parse(amount) / 100;
            }
        }

        /// <summary>
        ///     Gets the <c>DateTime</c> from string.
        /// </summary>
        /// <param name="value">The date time value.</param>
        /// <returns>
        ///     <c>DateTime</c> of string value.
        /// </returns>
        public static DateTime GetDate(string value)
        {
            bool isDateFormat = value.Length == PatternEnum.Date.Length;

            DateTime.TryParseExact(
                value,
                isDateFormat ? PatternEnum.Date : PatternEnum.DateTime,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime date);

            return date;
        }
    }

}
