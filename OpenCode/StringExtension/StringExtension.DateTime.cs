using System;
using System.Globalization;

namespace OpenCode;

/// <summary>
/// Date and time parsing and formatting helpers.
/// </summary>
/// <remarks>
/// Provides extension methods for converting and validating date/time-related string values.
/// These helpers use <see cref="CultureInfo.InvariantCulture"/> and <see cref="DateTimeStyles.None"/>
/// for consistent, culture-independent parsing.
/// </remarks>
public static partial class StringExtension
{
    /// <summary>
    /// Parses the specified string to a <see cref="DateTime"/>. If parsing fails, returns the provided default value.
    /// </summary>
    /// <param name="input">The string value to parse.</param>
    /// <param name="defaultValue">Value to return when parsing fails. Defaults to <see cref="DateTime.MinValue"/> when not provided.</param>
    /// <returns>The parsed <see cref="DateTime"/>, or <paramref name="defaultValue"/> when parsing fails.</returns>
    /// <example>
    /// <code>
    /// var dt = "2023-08-01".ParseToDateTime();
    /// </code>
    /// </example>
    public static DateTime ParseToDateTime(this string? input, DateTime defaultValue = default)
    => DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result)
    ? result
    : defaultValue;

    /// <summary>
    /// Determines whether the specified string can be parsed as a valid <see cref="DateTime"/> value.
    /// </summary>
    /// <param name="input">The string to validate.</param>
    /// <returns><c>true</c> if <paramref name="input"/> can be parsed to a <see cref="DateTime"/>; otherwise <c>false</c>.</returns>
    /// <example>
    /// <code>
    /// var isDate = "2023-08-01".IsDateTime(); // true
    /// </code>
    /// </example>
    public static bool IsDateTime(this string? input)
    => DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

    /// <summary>
    /// Converts the specified string to a formatted date string using the provided format.
    /// </summary>
    /// <param name="input">The string to parse and format.</param>
    /// <param name="format">A .NET standard or custom date and time format string. Defaults to "yyyy-MM-dd".</param>
    /// <returns>
    /// The formatted date string when parsing succeeds; otherwise returns the original input or an empty string when <paramref name="input"/> is <c>null</c>.
    /// </returns>
    /// <example>
    /// <code>
    /// var formatted = "2023-08-01T14:30:00".ToFormattedDate("yyyy/MM/dd"); // "2023/08/01"
    /// </code>
    /// </example>
    public static string ToFormattedDate(this string? input, string format = "yyyy-MM-dd")
    {
        if (DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            return date.ToString(format, CultureInfo.InvariantCulture);
        return input ?? string.Empty;
    }

    /// <summary>
    /// Parses the specified string to a <see cref="DateOnly"/> value. If parsing fails, returns the provided default value.
    /// </summary>
    /// <param name="input">The string value to parse.</param>
    /// <param name="defaultValue">Value to return when parsing fails. Defaults to <see cref="DateOnly"/> default when not provided.</param>
    /// <returns>The parsed <see cref="DateOnly"/>, or <paramref name="defaultValue"/> when parsing fails.</returns>
    /// <remarks>
    /// <para>This method internally parses the input to <see cref="DateTime"/> and then converts to <see cref="DateOnly"/>.
    /// It requires a target framework that supports <see cref="DateOnly"/> (available in .NET6+).</para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var d = "2023-08-01".ParseToDateOnly();
    /// </code>
    /// </example>
    public static DateOnly ParseToDateOnly(this string? input, DateOnly defaultValue = default)
    {
        if (DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            return DateOnly.FromDateTime(date);
        return defaultValue;
    }
}
