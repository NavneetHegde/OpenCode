
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
    }
}
