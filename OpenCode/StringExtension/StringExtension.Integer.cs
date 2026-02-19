using System.Globalization;

namespace OpenCode;

/// <summary>
/// Integer-related parsing and formatting helpers.
/// </summary>
/// <remarks>
/// This partial static class contains extension methods that operate on string values
/// to parse integers, check integer validity, and convert numeric strings to
/// ordinal representations. Parsing methods use <see cref="CultureInfo.InvariantCulture"/>
/// to ensure consistent behavior regardless of system culture.
/// </remarks>
public static partial class StringExtension
{
    /// <summary>
    /// Parses the input string to an <see cref="int"/> or returns a provided default value.
    /// </summary>
    /// <param name="input">The string to parse. May be <c>null</c> or contain surrounding whitespace.</param>
    /// <param name="defaultValue">The value to return when parsing fails. Defaults to <c>0</c>.</param>
    /// <returns>
    /// The parsed <see cref="int"/> if parsing succeeds; otherwise <paramref name="defaultValue"/>.
    /// </returns>
    /// <remarks>
    /// The method trims the input before attempting to parse and uses
    /// <see cref="NumberStyles.Any"/> with <see cref="CultureInfo.InvariantCulture"/>.
    /// Use this when you want a safe parse that never throws.
    /// </remarks>
    public static int ParseToInt(this string? input, int defaultValue = default)
    => int.TryParse(input?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
    ? result
    : defaultValue;

    /// <summary>
    /// Parses the input string to a <see cref="long"/> or returns a provided default value.
    /// </summary>
    /// <param name="input">The string to parse. May be <c>null</c> or contain surrounding whitespace.</param>
    /// <param name="defaultValue">The value to return when parsing fails. Defaults to <c>0L</c>.</param>
    /// <returns>
    /// The parsed <see cref="long"/> if parsing succeeds; otherwise <paramref name="defaultValue"/>.
    /// </returns>
    /// <remarks>
    /// The method trims the input before attempting to parse and uses
    /// <see cref="NumberStyles.Any"/> with <see cref="CultureInfo.InvariantCulture"/>.
    /// </remarks>
    public static long ParseToLong(this string? input, long defaultValue = default)
    => long.TryParse(input?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
    ? result
    : defaultValue;

    /// <summary>
    /// Determines whether the specified string represents a valid <see cref="int"/> value.
    /// </summary>
    /// <param name="input">The string to validate. May be <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="input"/> can be parsed as an <see cref="int"/> using
    /// <see cref="NumberStyles.Integer"/> and <see cref="CultureInfo.InvariantCulture"/>; otherwise <c>false</c>.
    /// </returns>
    /// <remarks>
    /// The method trims the input before attempting to parse, consistent with <see cref="ParseToInt"/>.
    /// </remarks>
    public static bool IsInteger(this string? input)
    => int.TryParse(input?.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out _);

    /// <summary>
    /// Converts a numeric string to its ordinal representation (for example, "1" -> "1st").
    /// </summary>
    /// <param name="input">The numeric string to convert. If parsing fails the original string (or empty string if <c>null</c>) is returned.</param>
    /// <returns>
    /// The ordinal representation for the parsed integer, or the original input (or empty string) when parsing fails.
    /// </returns>
    /// <remarks>
    /// The conversion follows English ordinal rules ("st", "nd", "rd", "th").
    /// Negative numbers are supported and will include the sign (for example, "-1" -> "-1st").
    /// This method does not trim the input before parsing; callers may trim if needed.
    /// </remarks>
    /// <example>
    /// <code>
    /// "1".ToOrdinal(); // "1st"
    /// "22".ToOrdinal(); // "22nd"
    /// "11".ToOrdinal(); // "11th"
    /// "abc".ToOrdinal(); // "abc"
    /// </code>
    /// </example>
    public static string ToOrdinal(this string? input)
    {
        if (!int.TryParse(input, out var number)) return input ?? string.Empty;

        return (number % 100) switch
        {
            11 or 12 or 13 => $"{number}th",
            _ => (number % 10) switch
            {
                1 => $"{number}st",
                2 => $"{number}nd",
                3 => $"{number}rd",
                _ => $"{number}th"
            }
        };
    }
}
