
namespace OpenCode
{
    public static partial class StringExtension
    {
        /// <summary>
        /// Returns parsed value or default value if parsing failed
        /// </summary>
        /// <param name="input">string to parse</param>
        /// <param name="defaultValue">default value if not passed then default(decimal)</param>
        /// <returns>Parsevalue or default value</returns>
        public static decimal ParseToDecimal(this string input, decimal defaultValue = default(decimal))
        {
            decimal output;
            if (decimal.TryParse(input, out output))
                return output;
            else
                return defaultValue;
        }
    }
}
