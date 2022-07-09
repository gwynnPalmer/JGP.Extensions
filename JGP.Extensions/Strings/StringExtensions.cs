// ***********************************************************************
// Assembly         : JGP.Extensions
// Author           : Joshua Gwynn-Palmer
// Created          : 06-17-2022
//
// Last Modified By : Joshua Gwynn-Palmer
// Last Modified On : 06-17-2022
// ***********************************************************************
// <copyright file="StringExtensions.cs" company="JGP.Extensions">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using JGP.Extensions.Collections;

namespace JGP.Extensions.Strings
{
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     Class StringExtensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Appends the prefix to the start of the string if the string does not already start with prefix.
        /// </summary>
        /// <param name="value">string to append prefix</param>
        /// <param name="prefix">prefix</param>
        /// <param name="ignoreCase">Indicates whether the compare should ignore case</param>
        /// <returns>System.String.</returns>
        public static string? AppendPrefixIfMissing(this string? value, string prefix, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(value)
                || (ignoreCase 
                    ? value.StartsWithIgnoreCase(prefix) 
                    : value.StartsWith(prefix)))
            {
                return value;
            }
            return prefix + value;
        }

        /// <summary>
        ///     Appends the suffix to the end of the string if the string does not already end in the suffix.
        /// </summary>
        /// <param name="value">string to append suffix</param>
        /// <param name="suffix">suffix</param>
        /// <param name="ignoreCase">Indicates whether the compare should ignore case</param>
        /// <returns>System.String.</returns>
        public static string? AppendSuffixIfMissing(this string? value, string suffix, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(value)
                || (ignoreCase
                    ? value.EndsWithIgnoreCase(suffix)
                    : value.EndsWith(suffix)))
            {
                return value;
            }
            return value + suffix;
        }

        /// <summary>
        ///     Read in a sequence of words from standard input and capitalize each
        ///     one (make first letter uppercase; make rest lowercase).
        /// </summary>
        /// <param name="value">string</param>
        /// <returns>Word with capitalization</returns>
        public static string Capitalize(this string value)
        {
            if (value.Length == 0) return value;
            return value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();
        }

        /// <summary>
        ///     Count number of occurrences in string
        /// </summary>
        /// <param name="value">string containing text</param>
        /// <param name="stringToMatch">string or pattern find</param>
        /// <returns>System.Int32.</returns>
        public static int CountOccurrences(this string value, string stringToMatch)
        {
            return Regex.Matches(value, stringToMatch, RegexOptions.IgnoreCase).Count;
        }

        /// <summary>
        ///     Convert string to Hash using MD5
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string CreateHashMD5(this string value)
        {
            // Use input string to calculate MD5 hash
            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            var stringBuilder = new StringBuilder();
            foreach (var b in hashBytes) stringBuilder.Append(b.ToString("X2"));
            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Convert string to Hash using Sha256
        /// </summary>
        /// <param name="value">string to hash</param>
        /// <returns>Hashed string</returns>
        /// <exception cref="System.ArgumentNullException">value</exception>
        public static string CreateHashSha256(this string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            var stringBuilder = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                var bytes = hash.ComputeHash(value.ToBytes());
                foreach (var b in bytes) stringBuilder.Append(b.ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Convert string to Hash using Sha512
        /// </summary>
        /// <param name="value">string to hash</param>
        /// <returns>Hashed string</returns>
        /// <exception cref="System.ArgumentNullException">value</exception>
        public static string CreateHashSha512(this string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            var stringBuilder = new StringBuilder();
            using (var hash = SHA512.Create())
            {
                var bytes = hash.ComputeHash(value.ToBytes());
                foreach (var b in bytes) stringBuilder.Append(b.ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Decrypt a string using the supplied key. Decoding is done using RSA encryption.
        /// </summary>
        /// <param name="stringToDecrypt">String that must be decrypted.</param>
        /// <param name="key">Decryption key.</param>
        /// <returns>The decrypted string or null if decryption failed.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToDecrypt or key is null or empty.</exception>
        public static string Decrypt(this string stringToDecrypt, string key)
        {
            var cspParameters = new CspParameters { KeyContainerName = key };
            var rsaServiceProvider = new RSACryptoServiceProvider(cspParameters) { PersistKeyInCsp = true };
            var decryptArray = stringToDecrypt.Split(new[] { "-" }, StringSplitOptions.None);
            var decryptByteArray =
                Array.ConvertAll(decryptArray, s => Convert.ToByte(byte.Parse(s, NumberStyles.HexNumber)));
            var bytes = rsaServiceProvider.Decrypt(decryptByteArray, true);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        ///     Check if a string does not end with prefix
        /// </summary>
        /// <param name="value">string to evaluate</param>
        /// <param name="suffix">suffix</param>
        /// <returns>true if string does not match prefix else false, null values will always evaluate to false</returns>
        public static bool DoesNotEndWith(this string? value, string? suffix)
        {
            return value == null
                   || suffix == null
                   || !value.EndsWith(suffix, StringComparison.InvariantCulture);
        }

        /// <summary>
        ///     Check if a string does not start with prefix
        /// </summary>
        /// <param name="value">string to evaluate</param>
        /// <param name="prefix">prefix</param>
        /// <returns>true if string does not match prefix else false, null values will always evaluate to false</returns>
        public static bool DoesNotStartWith(this string? value, string? prefix)
        {
            return value == null
                   || prefix == null
                   || !value.StartsWith(prefix, StringComparison.InvariantCulture);
        }

        /// <summary>
        ///     Encrypt a string using the supplied key. Encoding is done using RSA encryption.
        /// </summary>
        /// <param name="stringToEncrypt">String that must be encrypted.</param>
        /// <param name="key">Encryption key</param>
        /// <returns>A string representing a byte array separated by a minus sign.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToEncrypt or key is null or empty.</exception>
        public static string Encrypt(this string stringToEncrypt, string key)
        {
            var cspParameter = new CspParameters { KeyContainerName = key };
            var rsaServiceProvider = new RSACryptoServiceProvider(cspParameter) { PersistKeyInCsp = true };
            var bytes = rsaServiceProvider.Encrypt(Encoding.UTF8.GetBytes(stringToEncrypt), true);
            return BitConverter.ToString(bytes);
        }

        /// <summary>
        ///     Check a String ends with another string ignoring the case.
        /// </summary>
        /// <param name="value">string</param>
        /// <param name="suffix">suffix</param>
        /// <returns>true or false</returns>
        /// <exception cref="System.ArgumentNullException">value - value parameter is null</exception>
        /// <exception cref="System.ArgumentNullException">suffix - suffix parameter is null</exception>
        public static bool EndsWithIgnoreCase(this string value, string suffix)
        {
            if (value == null) throw new ArgumentNullException("value", "value parameter is null");
            if (suffix == null) throw new ArgumentNullException("suffix", "suffix parameter is null");
            return value.Length >= suffix.Length && value.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        ///     Gets first character in string
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>System.string</returns>
        public static string? FirstCharacter(this string? value)
        {
            return !string.IsNullOrEmpty(value)
                ? value.Length >= 1 ? value.Substring(0, 1) : value
                : null;
        }

        /// <summary>
        ///     Generates the random string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>System.String.</returns>
        public static string GenerateRandomString(int length)
        {
            var random = new Random();
            string[] chars =
            {
                "0", "1", "2", "3", "4", "5", "6", "7", "8",
                "9", "a", "b", "c", "d", "e", "f", "g", "h",
                "i", "j", "k", "l", "m", "n", "o", "p", "q",
                "r", "s", "t", "u", "v", "w", "x", "y", "z"
            };

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Function returns a default String value if given value is null or empty
        /// </summary>
        /// <param name="value">String value to check if isEmpty</param>
        /// <param name="defaultValue">default value to return if String value isEmpty</param>
        /// <returns>returns either String value or default value if IsEmpty</returns>
        public static string GetDefaultIfEmpty(this string? value, string defaultValue)
        {
            if (string.IsNullOrEmpty(value)) return defaultValue;

            value = value.Trim();
            return value.Length > 0 ? value : defaultValue;
        }

        /// <summary>
        ///     Gets a deterministic hash code for a string value.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <returns>System.Int32.</returns>
        public static int GetDeterministicHashCode(this string value)
        {
            unchecked
            {
                var hash1 = (5381 << 16) + 5381;
                var hash2 = hash1;

                for (var i = 0; i < value.Length; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ value[i];
                    if (i == value.Length - 1) break;
                    hash2 = ((hash2 << 5) + hash2) ^ value[i + 1];
                }

                return hash1 + hash2 * 1566083941;
            }
        }

        /// <summary>
        ///     Gets empty String if passed value is of type Null/Nothing
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>System.String</returns>
        public static string GetEmptyStringIfNull(this string? value)
        {
            return value != null ? value.Trim() : "";
        }

        /// <summary>
        ///     Gets the number of characters in string checks if string is null
        /// </summary>
        /// <param name="value">string to evaluate length</param>
        /// <returns>total number of chars or null if string is null</returns>
        public static int? GetLength(this string value)
        {
            return value?.Length;
        }

        /// <summary>
        ///     Checks if a string is null and returns String if not Empty else returns null/Nothing
        /// </summary>
        /// <param name="myValue">String value</param>
        /// <returns>null/nothing if String IsEmpty</returns>
        public static string? GetNullIfEmptyString(this string? myValue)
        {
            if (myValue == null || myValue.Length <= 0) return null;
            myValue = myValue.Trim();
            return myValue.Length > 0 ? myValue : null;
        }

        /// <summary>
        ///     Checks if the String contains only Unicode letters.
        ///     null will return false. An empty String ("") will return false.
        /// </summary>
        /// <param name="value">string to check if is Alpha</param>
        /// <returns>true if only contains letters, and is non-null</returns>
        public static bool IsAlpha(this string value)
        {
            return !string.IsNullOrEmpty(value) && value.Trim().Replace(" ", "").All(char.IsLetter);
        }

        /// <summary>
        ///     Checks if the String contains only Unicode letters, digits.
        ///     null will return false. An empty String ("") will return false.
        /// </summary>
        /// <param name="value">string to check if is Alpha or Numeric</param>
        /// <returns><c>true</c> if [is alpha numeric] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsAlphaNumeric(this string value)
        {
            return !string.IsNullOrEmpty(value) && value.Trim().Replace(" ", "").All(char.IsLetterOrDigit);
        }

        /// <summary>
        ///     Determines whether the specified value is date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is date; otherwise, <c>false</c>.</returns>
        public static bool IsDate(this string? value)
        {
            if (value?.Length < 8) return false;

            return !string.IsNullOrEmpty(value) && DateTime.TryParse(value, out _);
        }

        /// <summary>
        ///     Checks if date with dateFormat is parse-able to System.DateTime format returns boolean value if true else false
        /// </summary>
        /// <param name="value">String date</param>
        /// <param name="dateFormat">date format example dd/MM/yyyy HH:mm:ss</param>
        /// <returns>boolean True False if is valid System.DateTime</returns>
        public static bool IsDateTime(this string value, string dateFormat)
        {
            return DateTime.TryParseExact(value, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        /// <summary>
        ///     Validate email address
        /// </summary>
        /// <param name="email">string email address</param>
        /// <returns>true or false if email if valid</returns>
        public static bool IsEmailAddress(this string email)
        {
            const string pattern =
                "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
            return Regex.Match(email, pattern).Success;
        }

        /// <summary>
        ///     Determines whether the specified value is int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is int; otherwise, <c>false</c>.</returns>
        public static bool IsInt(this string? value)
        {
            return !string.IsNullOrEmpty(value) && int.TryParse(value, out _);
        }

        /// <summary>
        ///     Checks if string length satisfies minimum and maximum allowable char length. does not ignore leading and
        ///     trailing white-space
        /// </summary>
        /// <param name="value">string to evaluate</param>
        /// <param name="minCharLength">minimum char length</param>
        /// <param name="maxCharLength">maximum char length</param>
        /// <returns>true if string satisfies minimum and maximum allowable length</returns>
        public static bool IsLength(this string? value, int minCharLength, int maxCharLength)
        {
            return (value != null)
                   && (value.Length >= minCharLength)
                   && (value.Length <= maxCharLength);
        }

        /// <summary>
        ///     Checks if string length is consists of specified allowable maximum char length. does not ignore leading and
        ///     trailing white-space.
        ///     null strings will always evaluate to false.
        /// </summary>
        /// <param name="value">string to evaluate maximum length</param>
        /// <param name="maxCharLength">maximum allowable string length</param>
        /// <returns>true if string has specified maximum char length</returns>
        public static bool IsMaxLength(this string? value, int maxCharLength)
        {
            return value != null && value.Length <= maxCharLength;
        }

        /// <summary>
        ///     Checks if string length is a certain minimum number of characters, does not ignore leading and trailing
        ///     white-space.
        ///     null strings will always evaluate to false.
        /// </summary>
        /// <param name="value">string to evaluate minimum length</param>
        /// <param name="minCharLength">minimum allowable string length</param>
        /// <returns>true if string is of specified minimum length</returns>
        public static bool IsMinLength(this string? value, int minCharLength)
        {
            return value != null && value.Length >= minCharLength;
        }

        /// <summary>
        ///     Checks if a string is null
        /// </summary>
        /// <param name="value">string to evaluate</param>
        /// <returns>true if string is null else false</returns>
        public static bool IsNull(this string? value)
        {
            return value == null;
        }

        /// <summary>
        ///     Checks if a string is null or empty
        /// </summary>
        /// <param name="value">string to evaluate</param>
        /// <returns>true if string is null or is empty else false</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        ///     Determines whether [is null or white space] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is null or white space] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrWhiteSpace(this string? value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        ///     IsNumeric checks if a string is a valid floating value
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Boolean True if isNumeric else False</returns>
        public static bool IsNumeric(this string value)
        {
            return double.TryParse(value, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out _);
        }

        /// <summary>
        ///     Gets last character in string
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>System.string</returns>
        public static string? LastCharacter(this string? value)
        {
            return !string.IsNullOrEmpty(value)
                ? value.Length >= 1 ? value.Substring(value.Length - 1, 1) : value
                : null;
        }

        /// <summary>
        ///     Extracts the left part of the input string limited with the length parameter
        /// </summary>
        /// <param name="value">The input string to take the left part from</param>
        /// <param name="length">The total number characters to take from the input string</param>
        /// <returns>The substring starting at startIndex 0 until length</returns>
        /// <exception cref="System.ArgumentNullException">value</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     length - length cannot be higher than total string length or less
        ///     than 0
        /// </exception>
        public static string Left(this string value, int length)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            if (length < 0 || length > value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length),
                    "length cannot be higher than total string length or less than 0");
            }
            return value.Substring(0, length);
        }

        ///// <summary>
        /////     Normalizes the specified value.
        /////     Normalizes to NormalizationFormD where the char unicode category is not a Non Spacing Mark.
        ///// </summary>
        ///// <param name="value">The value.</param>
        ///// <returns>System.Nullable&lt;System.String&gt;.</returns>
        //public static string? Normalize(this string? value)
        //{
        //    if (value.IsNullOrWhiteSpace()) return null;
        //    return string.Concat(value
        //        .Normalize(NormalizationForm.FormD)
        //        .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
        //}

        ///// <summary>
        /////     Removes the characters.
        ///// </summary>
        ///// <param name="value">The value.</param>
        ///// <param name="characters">The characters.</param>
        ///// <returns>System.String.</returns>
        //public static string RemoveCharacters(this string value, IEnumerable<char> characters)
        //{
        //    var array = value.ToCharArray();
        //    array = Array.FindAll<char>(array, match => !characters.Contains(match));
        //    return new string(array);
        //}

        ///// <summary>
        /////     Removes the characters.
        ///// </summary>
        ///// <param name="value">The value.</param>
        ///// <param name="characters">The characters.</param>
        ///// <returns>System.String.</returns>
        //public static string? RemoveCharacters(this string? value, char[] characters)
        //{
        //    if (value.IsNullOrWhiteSpace()) return null;
        //    if (characters.Length <= 0) return value;
        //    var array = value.ToCharArray();
        //    array = Array.FindAll<char>(array, match => !characters.Contains(match));
        //    return new string(array);
        //}

        /// <summary>
        ///     Remove Characters from string
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="chars">The chars.</param>
        /// <returns>System.Nullable&lt;System.String&gt;.</returns>
        public static string? RemoveCharacters(this string? value, params char[] chars)
        {
            if (value.IsNullOrWhiteSpace()) return null;
            if (chars.Length <= 0) return value;
            var stringBuilder = new StringBuilder(value!.Length);
            foreach (var character in value.Where(c => !chars.Contains(c))) stringBuilder.Append(character);
            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Remove accent from strings
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>string without accents</returns>
        /// <example>
        ///     input:  "Příliš žluťoučký kůň úpěl ďábelské ódy."
        ///     result: "Prilis zlutoucky kun upel dabelske ody."
        /// </example>
        public static string RemoveDiacritics(this string value)
        {
            var stFormD = value.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var character in stFormD)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(character);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark) stringBuilder.Append(character);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        /// <summary>
        ///     Removes the first part of the string, if no match found return original string
        /// </summary>
        /// <param name="value">string to remove prefix</param>
        /// <param name="prefix">prefix</param>
        /// <param name="ignoreCase">Indicates whether the compare should ignore case</param>
        /// <returns>trimmed string with no prefix or original string</returns>
        public static string? RemovePrefix(this string? value, string prefix, bool ignoreCase = true)
        {
            if (!string.IsNullOrEmpty(value)
                && (ignoreCase
                    ? value.StartsWithIgnoreCase(prefix)
                    : value.StartsWith(prefix)))
            {
                return value.Substring(prefix.Length, value.Length - prefix.Length);
            }
            return value;
        }

        /// <summary>
        ///     Removes the end part of the string, if no match found return original string
        /// </summary>
        /// <param name="value">string to remove suffix</param>
        /// <param name="suffix">suffix</param>
        /// <param name="ignoreCase">Indicates whether the compare should ignore case</param>
        /// <returns>trimmed string with no suffix or original string</returns>
        public static string? RemoveSuffix(this string? value, string suffix, bool ignoreCase = true)
        {
            if (!string.IsNullOrEmpty(value)
                && (ignoreCase
                    ? value.EndsWithIgnoreCase(suffix)
                    : value.EndsWith(suffix)))
            {
                return value.Substring(0, value.Length - suffix.Length);
            }
            return value;
        }

        /// <summary>
        ///     Replace specified characters with an empty string.
        /// </summary>
        /// <param name="value">the string</param>
        /// <param name="chars">list of characters to replace from the string</param>
        /// <returns>System.string</returns>
        /// <example>
        ///     string value = "Friends";
        ///     value = value.Replace('F', 'r','i','value');  //value becomes 'ends';
        /// </example>
        public static string Replace(this string value, params char[] chars)
        {
            return chars
                .Aggregate(value, (current, c) => 
                    current.Replace(c.ToString(CultureInfo.InvariantCulture), ""));
        }

        /// <summary>
        ///     Reverse string
        /// </summary>
        /// <param name="value">string to reverse</param>
        /// <returns>System.string</returns>
        public static string Reverse(this string value)
        {
            var chars = new char[value.Length];
            for (int i = value.Length - 1, j = 0; i >= 0; --i, ++j) chars[j] = value[i];
            return new string(chars);
        }

        /// <summary>
        ///     Extracts the right part of the input string limited with the length parameter
        /// </summary>
        /// <param name="value">The input string to take the right part from</param>
        /// <param name="length">The total number characters to take from the input string</param>
        /// <returns>The substring taken from the input string</returns>
        /// <exception cref="System.ArgumentNullException">value</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     length - length cannot be higher than total string length or less
        ///     than 0
        /// </exception>
        public static string Right(this string value, int length)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
            if (length < 0 || length > value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length),
                    "length cannot be higher than total string length or less than 0");
            }            
            return value.Substring(value.Length - length);
        }

        /// <summary>
        ///     Check a String starts with another string ignoring the case.
        /// </summary>
        /// <param name="val">string</param>
        /// <param name="prefix">prefix</param>
        /// <returns>true or false</returns>
        /// <exception cref="System.ArgumentNullException">val - value parameter is null</exception>
        /// <exception cref="System.ArgumentNullException">prefix - prefix parameter is null</exception>
        public static bool StartsWithIgnoreCase(this string? val, string prefix)
        {
            if (val == null) throw new ArgumentNullException("val", "value parameter is null");
            if (prefix == null) throw new ArgumentNullException("prefix", "prefix parameter is null");
            return val.Length >= prefix.Length && val.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        ///     Convert a string to its equivalent byte array
        /// </summary>
        /// <param name="value">string to convert</param>
        /// <returns>System.byte array</returns>
        public static byte[] ToBytes(this string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        /// <summary>
        ///     Converts string to its Enum type
        ///     Checks of string is a member of type T enum before converting
        ///     if fails returns default enum
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="value">The string representation of the enumeration name or underlying value to convert</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Enum object</returns>
        /// <exception cref="System.ArgumentException">Type T Must of type System.Enum</exception>
        /// <remarks>
        ///     <exception cref="ArgumentException">
        ///         enumType is not an System.Enum.-or- value is either an empty string ("") or
        ///         only contains white space.-or- value is a name, but not one of the named constants defined for the enumeration
        ///     </exception>
        /// </remarks>
        public static T ToEnum<T>(this string value, T defaultValue = default) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("Type T Must of type System.Enum");

            var isParsed = Enum.TryParse(value, true, out T result);
            return isParsed ? result : defaultValue;
        }

        /// <summary>
        ///     Converts from Pascal Case to spaced words
        /// </summary>
        /// <param name="value">The input string.</param>
        /// <returns>System.String.</returns>
        public static string ToSpacedWords(this string value)
        {
            return string.Concat(value.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }
        
        /// <summary>
        ///     Truncates the string to the specified length.
        /// </summary>
        /// <param name="value">String to be truncated</param>
        /// <param name="maxLength">number of chars to truncate</param>
        /// <returns>System.String.</returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value) || maxLength <= 0) return string.Empty;
            return value.Length > maxLength ? value.Substring(0, maxLength) : value;
        }

        /// <summary>
        ///     Tries the get date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <param name="formattedString">The formatted string.</param>
        /// <returns><c>true</c> if value is <see cref="DateTime"/>, <c>false</c> otherwise.</returns>
        public static bool TryGetDateTime(this string? value, string format, out string? formattedString)
        {
            var success = DateTime.TryParse(value, out var dateValue);

            if (!success)
            {
                formattedString = null;
                return false;
            }

            formattedString = dateValue.ToString(format);
            return true;
        }
    }
}