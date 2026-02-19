using System;

namespace OpenCode;

/// <summary>
/// Guid conversion and validation helpers.
/// </summary>
/// <remarks>
/// Extension methods for parsing and validating values that represent <see cref="Guid"/> instances.
/// These helpers are null-safe and do not throw; they use <see cref="Guid.TryParse(string?, out Guid)"/> internally.
/// </remarks>
public static partial class StringExtension
{
    /// <summary>
    /// Parses the specified string to a <see cref="Guid"/> or returns the provided default value when parsing fails.
    /// </summary>
    /// <param name="input">The input string to parse. May be <c>null</c>.</param>
    /// <param name="defaultValue">The value to return when <paramref name="input"/> is <c>null</c> or not a valid GUID. Defaults to <see cref="Guid.Empty"/> when not supplied.</param>
    /// <returns>
    /// The parsed <see cref="Guid"/> when <paramref name="input"/> contains a valid GUID representation; otherwise <paramref name="defaultValue"/>.
    /// </returns>
    /// <example>
    /// <code>
    /// var g = "d3b07384-d9a1-4b6e-9a3f-8fc2f0a7b1ff".ParseToGuid();
    /// // g is the parsed Guid
    ///
    /// var fallback = "not-a-guid".ParseToGuid(Guid.NewGuid());
    /// // fallback is the provided default Guid because parsing failed
    /// </code>
    /// </example>
    /// <remarks>
    /// This method never throws; invalid input results in the returned default value.
    /// </remarks>
    public static Guid ParseToGuid(this string? input, Guid defaultValue = default)
    => Guid.TryParse(input, out var result) ? result : defaultValue;

    /// <summary>
    /// Determines whether the provided string is a valid GUID representation.
    /// </summary>
    /// <param name="input">The input string to test. May be <c>null</c>.</param>
    /// <returns><c>true</c> when <paramref name="input"/> contains a valid GUID representation; otherwise <c>false</c>.</returns>
    /// <example>
    /// <code>
    /// var isValid = "d3b07384-d9a1-4b6e-9a3f-8fc2f0a7b1ff".IsGuid(); // true
    /// var isNot = "hello".IsGuid(); // false
    /// </code>
    /// </example>
    public static bool IsGuid(this string? input)
    => Guid.TryParse(input, out _);

    /// <summary>
    /// Returns <c>true</c> if the input string is a valid GUID representation and is not <see cref="Guid.Empty"/>.
    /// </summary>
    /// <param name="input">The input string to test. May be <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> when <paramref name="input"/> contains a valid GUID representation that is not equal to <see cref="Guid.Empty"/>; otherwise <c>false</c>.
    /// </returns>
    /// <example>
    /// <code>
    /// var validNonEmpty = "d3b07384-d9a1-4b6e-9a3f-8fc2f0a7b1ff".IsValidGuid(); // true
    /// var emptyGuid = "00000000-0000-0000-0000-000000000000".IsValidGuid(); // false
    /// </code>
    /// </example>
    public static bool IsValidGuid(this string? input)
    => Guid.TryParse(input, out var g) && g != Guid.Empty;

    /// <summary>
    /// Decodes a 22-character URL-safe Base64 string (produced by <see cref="GuidExtension.ToShortGuid"/>) back to a <see cref="Guid"/>.
    /// </summary>
    /// <param name="input">The short GUID string to decode. Must be exactly 22 characters.</param>
    /// <param name="defaultValue">The value to return when decoding fails. Defaults to <see cref="Guid.Empty"/>.</param>
    /// <returns>The decoded <see cref="Guid"/>, or <paramref name="defaultValue"/> when input is invalid.</returns>
    public static Guid FromShortGuid(this string? input, Guid defaultValue = default)
    {
        if (string.IsNullOrEmpty(input) || input.Length != 22)
            return defaultValue;

        try
        {
            var base64 = input.Replace("_", "/").Replace("-", "+") + "==";
            var bytes = Convert.FromBase64String(base64);
            return new Guid(bytes);
        }
        catch (FormatException)
        {
            return defaultValue;
        }
    }
}
