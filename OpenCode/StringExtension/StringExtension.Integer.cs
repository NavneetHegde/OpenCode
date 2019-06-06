
namespace OpenCode
{
    public static partial class StringExtension
    {
        /// <summary>
        /// Returns parsed value or default value if parsing failed
        /// </summary>
        /// <param name="input">string to parse</param>
        /// <param name="defaultValue">default value if not passed then default(int)</param>
        /// <returns>Parse value or default value</returns>
        public static int ParseToInt(this string input, int defaultValue = default(int))
        {
            int output;
            if (int.TryParse(input, out output))
                return output;
            else
                return defaultValue;
        }
    }
}
