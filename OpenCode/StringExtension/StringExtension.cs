
using System;
using System.Security.Cryptography;

namespace OpenCode
{
    /// <summary>
    /// Specialized or generic extension for string type
    /// </summary>
    public static partial class StringExtension
    {
        /// <summary>
        /// Removes the trailing zeros from the string. if string is not in 
        /// proper ecimal format then function returns the same string value
        /// </summary>
        /// <param name="input">string value containig trailing zero</param>
        /// <returns>Compacted string value</returns>
        /// <example>2.0100 -> 2.01, 2.000 -> 2</example>
        public static string RemoveTrailingZero(this string input)
        {
            decimal output;
            if (decimal.TryParse(input, out output))
                return output.ToString("G29");
            else
                return input;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string TestRemoveTrailingZero(this string input)
        {
            decimal output;
            if (decimal.TryParse(input, out output))
                return output.ToString("G29");
            else
                return input;
        }

        /// <summary>
        /// Returns SHA256 hash of input string
        /// </summary>
        /// <param name="input">input to be hashed</param>
        /// <param name="hasHashed">True if hash is successful else false</param>
        /// <returns>Hashed value</returns>
        public static string SHA256tHash(this string input, out bool hasHashed)
        {
            hasHashed = false;
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            else
            {
                using (SHA256 hashAlgorithm = SHA256.Create())
                {
                    var byteValue = System.Text.Encoding.UTF8.GetBytes(input);
                    var byteHash = hashAlgorithm.ComputeHash(byteValue);
                    hasHashed = true;
                    return Convert.ToBase64String(byteHash, Base64FormattingOptions.None);
                }
            }
        }
    }
}
