
namespace OpenCode
{
    public static partial class StringExtension
    {
        /// <summary>
        /// Returns parsed value or default value if parsing failed
        /// </summary>
        /// <param name="input">string to parse</param>
        /// <param name="defaultValue">default value if not passed then default(bool)</param>
        /// <returns>Parse value or default value</returns>
        public static bool ParseToBool(this string input, bool defaultValue = default(bool))
        {
            bool output;
            if (bool.TryParse(input, out output))
                return output;
            else
                return defaultValue;
        }
    }
}
