using System;
using System.Security.Cryptography;
using System.Text;

namespace OpenCode;

public static partial class StringExtension
{
    /// <summary>
    /// Computes the MD5 hash of the provided string and returns a lowercase hexadecimal representation.
    /// </summary>
    /// <param name="input">The input string to hash. If <c>null</c> or empty, an empty string is returned.</param>
    /// <returns>Lowercase hexadecimal string of the MD5 hash, or empty string when input is null or empty.</returns>
    /// <remarks>
    /// Uses <see cref="MD5.HashData(byte[])"/> to compute the raw hash bytes. For .NET8+ the
    /// implementation uses <see cref="Convert.ToHexString(byte[])"/> and normalizes to lower-case to
    /// ensure consistent results across target frameworks. For earlier frameworks a manual conversion
    /// to hex is performed using <c>ToString("x2")</c> for each byte.
    /// </remarks>
    public static string ToMD5Hash(this string? input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        // Compute raw MD5 bytes from UTF-8 encoding of the input
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(input));

#if NET8_0_OR_GREATER
        // Convert raw bytes to hex and normalize to lowercase for consistency
        return Convert.ToHexString(hash).ToLowerInvariant();
#else
        // Fallback for older TFMs: build lowercase hex manually
        var sb = new StringBuilder(hash.Length * 2);
        foreach (var b in hash)
            sb.Append(b.ToString("x2")); // lowercase hex
        return sb.ToString();
#endif
    }

    /// <summary>
    /// Computes the SHA-256 hash of the provided string and returns it as a Base64-encoded string.
    /// </summary>
    /// <param name="input">The input string to hash. If <c>null</c> or whitespace, an empty string is returned.</param>
    /// <returns>Base64 encoded SHA-256 hash, or empty string when input is null or whitespace.</returns>
    public static string ToSHA256Hash(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Encodes the input string as Base64 using UTF-8 encoding.
    /// </summary>
    /// <param name="input">The input string to encode. If <c>null</c> or empty, an empty string is returned.</param>
    /// <returns>Base64 encoded representation of the input string, or empty string for null/empty input.</returns>
    public static string ToBase64(this string? input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        var bytes = Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Attempts to decode a Base64-encoded string using UTF-8. If decoding fails, returns the original input.
    /// </summary>
    /// <param name="input">The Base64 encoded string to decode. If <c>null</c> or empty, an empty string is returned.</param>
    /// <returns>Decoded UTF-8 string, or the original input if the input is not valid Base64.</returns>
    public static string FromBase64(this string? input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        try
        {
            var bytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            // If input is not valid Base64, return it unchanged
            return input;
        }
    }
}
