using System;
using System.Collections.Generic;

namespace OpenCode;

/// <summary>
/// Boolean parsing and conversion extensions for string.
/// </summary>
public static partial class StringExtension
{
    /// <summary>
    /// A set of additional string tokens recognised as boolean values.
    /// Comparison is case-insensitive (StringComparer.OrdinalIgnoreCase).
    /// Contains affirmative and negative short forms and numeric1/0.
    /// </summary>
    private static readonly HashSet<string> BoolStrings = new(StringComparer.OrdinalIgnoreCase)
    {
        "1", "0", "yes", "no", "y", "n", "on", "off"
    };

    /// <summary>
    /// Converts string to boolean. Accepts values like true/false,1/0, yes/no.
    /// </summary>
    /// <param name="input">Input string to parse. Can be null or whitespace.</param>
    /// <param name="defaultValue">Default value to return when input cannot be interpreted as boolean.</param>
    /// <returns>Parsed boolean value or <paramref name="defaultValue"/> if parsing fails.</returns>
    public static bool ParseToBool(this string? input, bool defaultValue = default)
    {
        // Return default when input is null, empty or only whitespace.
        if (string.IsNullOrWhiteSpace(input))
            return defaultValue;

        // First try the built-in boolean parser which recognizes "True"/"False" (case-insensitive).
        if (bool.TryParse(input?.Trim(), out var result))
            return result;

        // Normalize input and handle additional tokens not parsed by bool.TryParse.
        return input?.Trim().ToLowerInvariant() switch
        {
            // Affirmative tokens
            "1" or "yes" or "y" or "on" => true,
            // Negative tokens
            "0" or "no" or "n" or "off" => false,
            // Fallback to the provided default value for unknown tokens
            _ => defaultValue
        };
    }

    /// <summary>
    /// Checks whether a string can be interpreted as a boolean.
    /// Recognizes "true"/"false" and the additional tokens defined in <see cref="BoolStrings"/>.
    /// </summary>
    /// <param name="input">Input string to check. Can be null.</param>
    /// <returns>True if the string represents a boolean value; otherwise false.</returns>
    public static bool IsBool(this string? input)
    {
        // Null or whitespace cannot be interpreted as boolean.
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var trimmed = input.Trim();

        // Check built-in boolean parsing or presence in the additional tokens set.
        return bool.TryParse(trimmed, out _) || BoolStrings.Contains(trimmed);
    }

    /// <summary>
    /// Converts "true"/"false" strings (and supported tokens) into "Yes"/"No".
    /// Uses <see cref="ParseToBool(string?, bool)"/> to determine truthiness.
    /// </summary>
    /// <param name="input">Input string to convert.</param>
    /// <returns>"Yes" if the input parses to true; otherwise "No".</returns>
    public static string ToYesNo(this string? input)
        => input.ParseToBool() ? "Yes" : "No";
}
