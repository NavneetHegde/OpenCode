using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace OpenCode.Tests;

public class StringExtensionHashTests
{
    #region --- ToSHA256Hash ---

    [Fact]
    public void ToSHA256Hash_ValidString_ReturnsExpected()
    {
        string input = "Hello World";
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        var expected = ToHexLower(hash);

        Assert.Equal(expected, input.ToSHA256Hash());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ToSHA256Hash_NullOrEmpty_ReturnsEmpty(string? input)
    {
        Assert.Equal(string.Empty, input.ToSHA256Hash());
    }

    [Fact]
    public void ToSHA256Hash_WhitespaceString_ReturnsHash()
    {
        string input = "   ";
        var result = input.ToSHA256Hash();
        Assert.NotEqual(string.Empty, result);
    }

    #endregion

    #region --- ToMD5Hash ---

    private static string ToHexLower(byte[] bytes)
    {
        var sb = new StringBuilder(bytes.Length * 2);
        foreach (var b in bytes)
            sb.Append(b.ToString("x2")); // lowercase hex
        return sb.ToString();
    }

    [Fact]
    public void ToMD5Hash_ValidString_ReturnsExpected()
    {
        // Arrange
        string input = "Hello World";
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(input));
        var expected = ToHexLower(hash); // use helper

        // Act
        var actual = input.ToMD5Hash();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ToMD5Hash_NullOrEmpty_ReturnsEmpty(string? input)
    {
        Assert.Equal(string.Empty, input.ToMD5Hash());
    }

    #endregion

    #region --- ToBase64 ---

    [Theory]
    [InlineData("Hello World", "SGVsbG8gV29ybGQ=")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void ToBase64_ReturnsExpected(string? input, string expected)
    {
        Assert.Equal(expected, input.ToBase64());
    }

    #endregion

    #region --- FromBase64 ---

    [Theory]
    [InlineData("SGVsbG8gV29ybGQ=", "Hello World")]
    [InlineData("", "")]
    [InlineData(null, "")]
    [InlineData("InvalidBase64", "InvalidBase64")] // return as-is
    public void FromBase64_ReturnsExpected(string? input, string expected)
    {
        Assert.Equal(expected, input.FromBase64());
    }

    #endregion
}
