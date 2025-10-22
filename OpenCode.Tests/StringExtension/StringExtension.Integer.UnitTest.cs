using Xunit;

namespace OpenCode.Tests;

public class StringExtensionIntegerTests
{
    #region --- ParseToInt ---

    [Theory]
    [InlineData("123", 123)]
    [InlineData("  456 ", 456)]
    [InlineData(null, 0)]
    [InlineData("", 0)]
    [InlineData("invalid", 0)]
    public void ParseToInt_ReturnsExpected(string? input, int expected)
    {
        Assert.Equal(expected, input.ParseToInt());
    }

    [Fact]
    public void ParseToInt_UsesCustomDefaultValue()
    {
        string? input = "abc";
        int defaultValue = 99;
        var result = input.ParseToInt(defaultValue);
        Assert.Equal(defaultValue, result);
    }

    #endregion

    #region --- ParseToLong ---

    [Theory]
    [InlineData("1234567890123", 1234567890123)]
    [InlineData("  9876543210 ", 9876543210)]
    [InlineData(null, 0)]
    [InlineData("", 0)]
    [InlineData("invalid", 0)]
    public void ParseToLong_ReturnsExpected(string? input, long expected)
    {
        Assert.Equal(expected, input.ParseToLong());
    }

    [Fact]
    public void ParseToLong_UsesCustomDefaultValue()
    {
        string? input = "abc";
        long defaultValue = 9999;
        var result = input.ParseToLong(defaultValue);
        Assert.Equal(defaultValue, result);
    }

    #endregion

    #region --- IsInteger ---

    [Theory]
    [InlineData("123", true)]
    [InlineData("-456", true)]
    [InlineData("0", true)]
    [InlineData("123.45", false)]
    [InlineData("abc", false)]
    [InlineData(null, false)]
    [InlineData("", false)]
    public void IsInteger_ReturnsExpected(string? input, bool expected)
    {
        Assert.Equal(expected, input.IsInteger());
    }

    #endregion

    #region --- ToOrdinal ---

    [Theory]
    [InlineData("1", "1st")]
    [InlineData("2", "2nd")]
    [InlineData("3", "3rd")]
    [InlineData("4", "4th")]
    [InlineData("11", "11th")]
    [InlineData("12", "12th")]
    [InlineData("13", "13th")]
    [InlineData("21", "21st")]
    [InlineData("22", "22nd")]
    [InlineData("23", "23rd")]
    [InlineData("101", "101st")]
    [InlineData("abc", "abc")]
    [InlineData(null, "")]
    public void ToOrdinal_ReturnsExpected(string? input, string expected)
    {
        Assert.Equal(expected, input.ToOrdinal());
    }

    #endregion
}
