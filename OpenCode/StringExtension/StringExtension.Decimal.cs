using System.Globalization;

namespace OpenCode;

/// <summary>
/// Decimal parsing, formatting, and numeric cleanup helpers.
/// </summary>
/// <remarks>
/// All parsing and formatting in this class uses <see cref="CultureInfo.InvariantCulture"/> to ensure
/// consistent behavior regardless of the current thread culture (for example decimal separator).
/// </remarks>
public static partial class StringExtension
{
    /// <summary>
    /// Parses the specified string to a <see cref="decimal"/>. If parsing fails, returns the provided
    /// <paramref name="defaultValue"/>.
    /// </summary>
    /// <param name="input">The string input to parse. Leading and trailing white-space are trimmed before parsing.</param>
    /// <param name="defaultValue">The value to return when parsing fails. Defaults to <c>default(decimal)</c>.</param>
    /// <returns>The parsed <see cref="decimal"/> value when parsing succeeds; otherwise <paramref name="defaultValue"/>.</returns>
    public static decimal ParseToDecimal(this string? input, decimal defaultValue = default)
        => decimal.TryParse(input?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var result)
            ? result
            : defaultValue;

    /// <summary>
    /// Removes unnecessary trailing zeros from a numeric string representation (for example "2.500" → "2.5").
    /// </summary>
    /// <param name="input">The string to normalize. If the string is not a valid decimal, the original string (or empty string when <c>null</c>) is returned.</param>
    /// <returns>
    /// A normalized numeric string without redundant trailing zeros when <paramref name="input"/> can be parsed to a <see cref="decimal"/>; otherwise the original input or an empty string when <c>null</c>.
    /// </returns>
    /// <remarks>
    /// Uses <c>decimal.TryParse</c> with <see cref="NumberStyles.Any"/> and <see cref="CultureInfo.InvariantCulture"/>.
    /// When parsing succeeds the result is formatted using the "G29" format specifier to avoid scientific notation and to remove
    /// redundant trailing zeros while preserving significant digits.
    /// </remarks>
    public static string RemoveTrailingZero(this string? input)
    {
        if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var output))
            return output.ToString("G29", CultureInfo.InvariantCulture);
        return input ?? string.Empty;
    }

    /// <summary>
    /// Determines whether the specified string can be parsed to a <see cref="decimal"/> value.
    /// </summary>
    /// <param name="input">The string to validate.</param>
    /// <returns><c>true</c> when <paramref name="input"/> can be parsed to a <see cref="decimal"/> using invariant culture; otherwise <c>false</c>.</returns>
    public static bool IsDecimal(this string? input)
        => decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
}
