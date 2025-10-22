using Xunit;

namespace OpenCode.Tests;

public class StringExtensionDecimalTests
{
    #region --- ParseToDecimal ---

    [Theory]
    [InlineData("123.45", 123.45)]
    [InlineData("  678.9 ", 678.9)]
    [InlineData(null, 0)]
    [InlineData("", 0)]
    [InlineData("invalid", 0)]
    public void ParseToDecimal_ReturnsExpected(string? input, decimal expected)
    {
        var result = input.ParseToDecimal();
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseToDecimal_UsesCustomDefaultValue()
    {
        string? input = "invalid";
        decimal defaultValue = 99.99m;
        var result = input.ParseToDecimal(defaultValue);
        Assert.Equal(defaultValue, result);
    }

    #endregion

    #region --- RemoveTrailingZero ---

    [Theory]
    [InlineData("2.500", "2.5")]
    [InlineData("123.0000", "123")]
    [InlineData("0.000", "0")]
    [InlineData("45.678900", "45.6789")]
    [InlineData(null, "")]
    [InlineData("invalid", "invalid")]
    public void RemoveTrailingZero_ReturnsExpected(string? input, string? expected)
    {
        var result = input.RemoveTrailingZero();
        Assert.Equal(expected, result);
    }

    #endregion

    #region --- IsDecimal ---

    [Theory]
    [InlineData("123.45", true)]
    [InlineData("0", true)]
    [InlineData("-987.65", true)]
    [InlineData("invalid", false)]
    [InlineData(null, false)]
    [InlineData("", false)]
    public void IsDecimal_ReturnsExpected(string? input, bool expected)
    {
        Assert.Equal(expected, input.IsDecimal());
    }

    #endregion
}
