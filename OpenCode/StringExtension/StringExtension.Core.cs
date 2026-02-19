using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenCode;

/// <summary>
/// Core, formatting, and utility extensions for string type.
/// Provides a set of safe and convenient helpers for working with strings,
/// including trimming, casing conversions, masking, validation and formatting.
/// </summary>
public static partial class StringExtension
{
    #region --- Core Helpers ---

    /// <summary>
    /// Returns null if the string is null or whitespace.
    /// </summary>
    /// <param name="input">The input string to check.</param>
    /// <returns>Null if <paramref name="input"/> is null, empty or consists only of white-space characters; otherwise returns the original string.</returns>
    public static string? NullIfEmpty(this string? input)
        => string.IsNullOrWhiteSpace(input) ? null : input;

    /// <summary>
    /// Returns default value if string is null or whitespace.
    /// </summary>
    /// <param name="input">The input string to check.</param>
    /// <param name="defaultValue">Value to return when <paramref name="input"/> is null, empty or white-space. Defaults to an empty string.</param>
    /// <returns><paramref name="defaultValue"/> when <paramref name="input"/> is null/whitespace; otherwise the original string.</returns>
    public static string OrDefault(this string? input, string defaultValue = "")
        => string.IsNullOrWhiteSpace(input) ? defaultValue : input;

    /// <summary>
    /// Trims safely — never throws, returns empty string if null.
    /// </summary>
    /// <param name="input">The input string to trim.</param>
    /// <returns>Trimmed string, or empty string when <paramref name="input"/> is null.</returns>
    public static string SafeTrim(this string? input)
        => input?.Trim() ?? string.Empty;

    /// <summary>
    /// Truncates string safely to specified length.
    /// </summary>
    /// <param name="input">The input string to truncate.</param>
    /// <param name="maxLength">Maximum allowed length of returned string. If less than or equal to the input length, the string will be cut to this length.</param>
    /// <returns>Truncated string if needed, original string if it is shorter than <paramref name="maxLength"/>, or empty string when <paramref name="input"/> is null.</returns>
    public static string Truncate(this string? input, int maxLength)
        => string.IsNullOrEmpty(input) || input.Length <= maxLength ? input ?? string.Empty : input[..maxLength];

    /// <summary>
    /// Checks if the string contains numeric characters only.
    /// </summary>
    /// <param name="input">The input string to validate.</param>
    /// <returns>True if <paramref name="input"/> can be parsed as a decimal using invariant culture; otherwise false. Null or empty strings return false.</returns>
    public static bool IsNumeric(this string? input)
        => decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out _);

    /// <summary>
    /// Compares two strings ignoring case.
    /// </summary>
    /// <param name="input">First string to compare.</param>
    /// <param name="other">Second string to compare.</param>
    /// <returns>True if both strings are equal using ordinal, case-insensitive comparison; otherwise false.</returns>
    public static bool EqualsIgnoreCase(this string? input, string? other)
        => string.Equals(input, other, StringComparison.OrdinalIgnoreCase);

    #endregion

    #region --- Format Helpers ---

    /// <summary>
    /// Converts to Title Case (each word capitalized).
    /// </summary>
    /// <param name="input">The input string to convert.</param>
    /// <returns>Title-cased string using invariant culture. Returns empty string when input is null or whitespace.</returns>
    public static string ToTitleCase(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(input.ToLowerInvariant());
    }

#if NET7_0_OR_GREATER
    [GeneratedRegex(@"[^A-Za-z0-9]+")]
    private static partial Regex NonAlphaNumericRegex();
#else
    private static readonly Regex NonAlphaNumericRegex = new(@"[^A-Za-z0-9]+", RegexOptions.Compiled);
#endif

    /// <summary>
    /// Converts various separators and non-alphanumeric characters into PascalCase.
    /// </summary>
    /// <param name="input">The input string to convert.</param>
    /// <returns>PascalCase version of the input. Returns empty string when input is null or whitespace.</returns>
    public static string ToPascalCase(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

#if NET7_0_OR_GREATER
        var words = NonAlphaNumericRegex().Split(input);
#else
        var words = NonAlphaNumericRegex.Split(input);
#endif

        var sb = new StringBuilder();
        foreach (var word in words)
        {
            if (word.Length == 0) continue;

            sb.Append(char.ToUpperInvariant(word[0]));
            if (word.Length > 1)
                sb.Append(word[1..].ToLowerInvariant());
        }

        return sb.ToString();
    }

    /// <summary>
    /// Converts to camelCase.
    /// </summary>
    /// <param name="input">The input string to convert.</param>
    /// <returns>camelCase version of the input. Returns empty string when input is null or whitespace.</returns>
    public static string ToCamelCase(this string? input)
    {
        var pascal = input.ToPascalCase();
        return string.IsNullOrEmpty(pascal)
            ? pascal
            : char.ToLowerInvariant(pascal[0]) + pascal[1..];
    }


#if NET7_0_OR_GREATER
[GeneratedRegex(@"([a-z0-9])([A-Z])")]
private static partial Regex CamelCaseRegex();

[GeneratedRegex(@"[\s\-]+")]
private static partial Regex SpaceOrDashRegex();

[GeneratedRegex(@"[\s_]+")]
private static partial Regex SpaceOrUnderscoreRegex();
#else
    private static readonly Regex CamelCaseRegex = new(@"([a-z0-9])([A-Z])", RegexOptions.Compiled);
    private static readonly Regex SpaceOrDashRegex = new(@"[\s\-]+", RegexOptions.Compiled);
    private static readonly Regex SpaceOrUnderscoreRegex = new(@"[\s_]+", RegexOptions.Compiled);
#endif

    /// <summary>
    /// Converts string to snake_case.
    /// </summary>
    /// <param name="input">The input string to convert.</param>
    /// <returns>snake_case version of the input. Returns empty string when input is null or whitespace.</returns>
    public static string ToSnakeCase(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

#if NET7_0_OR_GREATER
        var result = CamelCaseRegex().Replace(input, "$1_$2");
        result = SpaceOrDashRegex().Replace(result, "_");
#else
        var result = CamelCaseRegex.Replace(input, "$1_$2");
        result = SpaceOrDashRegex.Replace(result, "_");
#endif

        return result.ToLowerInvariant();
    }


    /// <summary>
    /// Converts to kebab-case.
    /// </summary>
    /// <param name="input">The input string to convert.</param>
    /// <returns>kebab-case (dash separated lowercase) version of the input. Returns empty string when input is null or whitespace.</returns>
    public static string ToKebabCase(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

#if NET7_0_OR_GREATER
        var result = CamelCaseRegex().Replace(input, "$1-$2");
        result = SpaceOrUnderscoreRegex().Replace(result, "-");
#else
        var result = CamelCaseRegex.Replace(input, "$1-$2");
        result = SpaceOrUnderscoreRegex.Replace(result, "-");
#endif
        return result.ToLowerInvariant();
    }

    /// <summary>
    /// Masks a string leaving given number of start and end characters visible.
    /// </summary>
    /// <param name="input">The input string to mask.</param>
    /// <param name="unmaskedStart">Number of characters to leave unmasked at the start.</param>
    /// <param name="unmaskedEnd">Number of characters to leave unmasked at the end.</param>
    /// <param name="maskChar">Character used for masking the middle portion.</param>
    /// <returns>Masked string where the middle portion is replaced with <paramref name="maskChar"/> repeated. If the input is shorter than or equal to the sum of unmasked characters, the original string is returned.</returns>
    public static string Mask(this string? input, int unmaskedStart = 1, int unmaskedEnd = 1, char maskChar = '*')
    {
        if (string.IsNullOrEmpty(input) || input.Length <= unmaskedStart + unmaskedEnd)
            return input ?? string.Empty;

        var middle = new string(maskChar, input.Length - (unmaskedStart + unmaskedEnd));
        return input![0..unmaskedStart] + middle + input[^unmaskedEnd..];
    }

    /// <summary>
    /// Formats a template string with positional parameters.
    /// </summary>
    /// <param name="template">Composite format string containing placeholders like {0}, {1}, etc.</param>
    /// <param name="args">Positional arguments to format into the template.</param>
    /// <returns>Formatted string using <see cref="CultureInfo.InvariantCulture"/>. Returns empty string when template is null or empty.</returns>
    public static string FormatWith(this string? template, params object[] args)
    {
        if (string.IsNullOrEmpty(template)) return string.Empty;
        return string.Format(CultureInfo.InvariantCulture, template, args);
    }

    #endregion

    #region --- Utility Helpers ---

    /// <summary>
    /// Returns a substring safely without exceptions.
    /// </summary>
    /// <param name="input">The input string from which to extract a substring.</param>
    /// <param name="startIndex">The zero-based starting character position of a substring in this instance. Values less than0 are treated as0.</param>
    /// <param name="length">The number of characters in the substring. If the requested length extends past the end of the string, it will be truncated to the available characters.</param>
    /// <returns>Requested substring or empty string if input is null, startIndex is beyond the end of the string, or input is empty.</returns>
    public static string SafeSubstring(this string? input, int startIndex, int length)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        if (startIndex < 0) startIndex = 0;
        if (startIndex >= input.Length) return string.Empty;
        if (length + startIndex > input.Length) length = input.Length - startIndex;
        return input.Substring(startIndex, length);
    }

    /// <summary>
    /// Removes diacritical marks (accents).
    /// </summary>
    /// <param name="input">The input string to normalize.</param>
    /// <returns>String with diacritical marks removed. Returns empty string when input is null or empty.</returns>
    public static string RemoveDiacritics(this string? input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        var normalized = input.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in normalized)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(c);
            if (category != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }


#if NET7_0_OR_GREATER
    [GeneratedRegex(@"[^a-zA-Z0-9]")]
    private static partial Regex NonAlphanumericSingleRegex();
#else
    private static readonly Regex NonAlphanumericSingleRegex = new(@"[^a-zA-Z0-9]", RegexOptions.Compiled);
#endif

    /// <summary>
    /// Removes any non-alphanumeric characters from the input string.
    /// </summary>
    /// <param name="input">The input string to sanitize.</param>
    /// <returns>String containing only alphanumeric characters. Returns empty string when input is null or empty.</returns>
    public static string RemoveNonAlphanumeric(this string? input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

#if NET7_0_OR_GREATER
        return NonAlphanumericSingleRegex().Replace(input, string.Empty);
#else
        return NonAlphanumericSingleRegex.Replace(input, string.Empty);
#endif
    }

    /// <summary>
    /// Returns a slugified (URL-safe) version of the string.
    /// </summary>
    /// <param name="input">The input string to convert to a slug.</param>
    /// <returns>Lowercase, URL-friendly slug with diacritics removed and spaces replaced by dashes. Returns empty string when input is null or whitespace.</returns>
    public static string ToSlug(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        input = input.RemoveDiacritics().ToLowerInvariant();
        input = Regex.Replace(input, @"[^a-z0-9\s-]", "");
        input = Regex.Replace(input, @"\s+", "-").Trim('-');
        return input;
    }

    /// <summary>
    /// Validates if the input string is in a simple email format.
    /// </summary>
    /// <param name="input">The input string to validate as email.</param>
    /// <returns>True if the input matches a basic email pattern; false otherwise. Null input is treated as empty string.</returns>
    public static bool IsEmail(this string? input)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(
                input ?? "",
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// Quick check to determine if a string looks like JSON (starts/ends with braces or brackets).
    /// </summary>
    /// <param name="input">The input string to check.</param>
    /// <returns>True if input is a non-empty trimmed string that starts with '{' and ends with '}' or starts with '[' and ends with ']'. Otherwise false.</returns>
    public static bool IsJson(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        input = input.Trim();
        return (input.StartsWith('{') && input.EndsWith('}')) ||
               (input.StartsWith('[') && input.EndsWith(']'));
    }

    /// <summary>
    /// Reverses the characters in the string, correctly handling Unicode surrogate pairs.
    /// </summary>
    /// <param name="input">The input string to reverse.</param>
    /// <returns>Reversed string. Returns empty string when input is null or empty.</returns>
    public static string Reverse(this string? input)
    {
        if (string.IsNullOrEmpty(input)) return string.Empty;
        var elements = System.Globalization.StringInfo.GetTextElementEnumerator(input);
        var result = new List<string>();
        while (elements.MoveNext())
            result.Add(elements.GetTextElement());
        result.Reverse();
        return string.Concat(result);
    }

    /// <summary>
    /// Determines whether the string contains the specified value using case-insensitive comparison.
    /// </summary>
    /// <param name="input">The input string to search within.</param>
    /// <param name="value">The value to search for.</param>
    /// <returns>True if <paramref name="input"/> contains <paramref name="value"/> using ordinal case-insensitive comparison; otherwise false.</returns>
    public static bool ContainsIgnoreCase(this string? input, string? value)
    {
        if (input is null || value is null) return false;
        return input.Contains(value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Returns the leftmost N characters of the string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="length">Number of characters to return from the start.</param>
    /// <returns>The first <paramref name="length"/> characters, or the full string if shorter. Returns empty string when input is null or empty.</returns>
    public static string Left(this string? input, int length)
    {
        if (string.IsNullOrEmpty(input) || length <= 0) return string.Empty;
        return length >= input.Length ? input : input[..length];
    }

    /// <summary>
    /// Returns the rightmost N characters of the string.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="length">Number of characters to return from the end.</param>
    /// <returns>The last <paramref name="length"/> characters, or the full string if shorter. Returns empty string when input is null or empty.</returns>
    public static string Right(this string? input, int length)
    {
        if (string.IsNullOrEmpty(input) || length <= 0) return string.Empty;
        return length >= input.Length ? input : input[^length..];
    }

    /// <summary>
    /// Counts the number of words in the string by splitting on whitespace.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>Number of words. Returns 0 for null, empty, or whitespace-only input.</returns>
    public static int WordCount(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return 0;
        return input.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    /// <summary>
    /// Ensures the string starts with the specified prefix. Prepends it if missing.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="prefix">The prefix to ensure.</param>
    /// <param name="comparison">The string comparison type. Defaults to <see cref="StringComparison.Ordinal"/>.</param>
    /// <returns>The string guaranteed to start with <paramref name="prefix"/>. Returns <paramref name="prefix"/> when input is null or empty.</returns>
    public static string EnsureStartsWith(this string? input, string prefix, StringComparison comparison = StringComparison.Ordinal)
    {
        if (string.IsNullOrEmpty(input)) return prefix;
        return input.StartsWith(prefix, comparison) ? input : prefix + input;
    }

    /// <summary>
    /// Ensures the string ends with the specified suffix. Appends it if missing.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="suffix">The suffix to ensure.</param>
    /// <param name="comparison">The string comparison type. Defaults to <see cref="StringComparison.Ordinal"/>.</param>
    /// <returns>The string guaranteed to end with <paramref name="suffix"/>. Returns <paramref name="suffix"/> when input is null or empty.</returns>
    public static string EnsureEndsWith(this string? input, string suffix, StringComparison comparison = StringComparison.Ordinal)
    {
        if (string.IsNullOrEmpty(input)) return suffix;
        return input.EndsWith(suffix, comparison) ? input : input + suffix;
    }

    #endregion
}
