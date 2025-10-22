using System;

namespace OpenCode;

/// <summary>
/// Provides extension methods for <see cref="Guid"/> formatting and validation.
/// </summary>
public static class GuidExtension
{
    /// <summary>
    /// Determines whether the specified <see cref="Guid"/> is not equal to <see cref="Guid.Empty"/>.
    /// </summary>
    /// <param name="guid">The <see cref="Guid"/> instance to check.</param>
    /// <returns><c>true</c> if <paramref name="guid"/> is not <see cref="Guid.Empty"/>; otherwise, <c>false</c>.</returns>
    public static bool IsNotEmpty(this Guid guid) => guid != Guid.Empty;

    /// <summary>
    /// Returns a shortened, URL-safe representation of the <see cref="Guid"/>.
    /// </summary>
    /// <param name="guid">The <see cref="Guid"/> to convert.</param>
    /// <returns>
    /// A 22-character, URL-safe Base64 string representing the <paramref name="guid"/>.
    /// Implementation details:
    /// - The GUID is converted to a 16-byte array and Base64-encoded (which normally yields 24 characters including padding).
    /// - Characters '/' and '+' are replaced with '_' and '-' respectively to make the string URL-safe.
    /// - The trailing padding characters are removed by taking the first 22 characters.
    /// 
    /// Example: a GUID encoded with this method produces a compact, URL-friendly identifier that can be
    /// decoded back to the original GUID by reversing the replacements, adding '==' padding, and Base64-decoding.
    /// </returns>
    public static string ToShortGuid(this Guid guid)
        => Convert.ToBase64String(guid.ToByteArray())
            .Replace("/", "_")
            .Replace("+", "-")
            .Substring(0, 22);

    /// <summary>
    /// Returns the string representation of the <see cref="Guid"/> without hyphens.
    /// </summary>
    /// <param name="guid">The <see cref="Guid"/> to format.</param>
    /// <returns>
    /// A 32-character hexadecimal string representation of the GUID using the "N" format specifier
    /// (all lowercase by default for <see cref="Guid.ToString(string)"/>).
    /// </returns>
    public static string ToCompactString(this Guid guid) => guid.ToString("N");

    /// <summary>
    /// Determines whether the specified <see cref="Guid"/> is equal to <see cref="Guid.Empty"/>.
    /// </summary>
    /// <param name="guid">The <see cref="Guid"/> instance to check.</param>
    /// <returns><c>true</c> if <paramref name="guid"/> is <see cref="Guid.Empty"/>; otherwise, <c>false</c>.</returns>
    public static bool IsEmpty(this Guid guid) => guid == Guid.Empty;
}
