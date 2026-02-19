using System.Globalization;
using Xunit;

namespace OpenCode.Tests;

public class DecimalExtensionTests
{
    [Theory]
    [InlineData(12.3456, 2, 12.35)]
    [InlineData(12.344, 2, 12.34)]
    [InlineData(-12.345, 1, -12.3)]
    public void RoundTo_ReturnsExpectedDecimal(decimal value, int decimals, decimal expected)
    {
        var result = value.RoundTo(decimals);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(12345.678, "N2", "12,345.68")]
    [InlineData(12345.678, "N0", "12,346")]
    [InlineData(12345.678, "C", "$12,345.68")] // using InvariantCulture will prefix with "¤"
    public void ToMoneyFormat_ReturnsExpectedString(decimal value, string format, string expected)
    {
        var result = value.ToMoneyFormat(format, CultureInfo.GetCultureInfo("en-US"));
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1.0000, 1.00005, true)]
    [InlineData(1.0000, 1.0002, false)]
    [InlineData(-1.0000, -1.00009, true)]
    public void AlmostEquals_ReturnsExpectedResult(decimal a, decimal b, bool expected)
    {
        var result = a.AlmostEquals(b);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0.25, 0, "25%")]
    [InlineData(0.2567, 1, "25.7%")]
    [InlineData(1, 0, "100%")]
    public void ToPercentage_ReturnsExpectedString(decimal value, int decimals, string expected)
    {
        var result = value.ToPercentage(decimals);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(12.34, "en-US", "$12.34")]
    [InlineData(12.34, "fr-FR", "12,34 €")]
    [InlineData(12.34, "ja-JP", "￥12")]
    public void ToCurrency_ReturnsExpectedString(decimal value, string cultureName, string expected)
    {
        var culture = CultureInfo.GetCultureInfo(cultureName);
        var result = value.ToCurrency(culture);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1.5, true)]
    [InlineData(0.001, true)]
    [InlineData(0, false)]
    [InlineData(-1.5, false)]
    public void IsPositive_ReturnsExpected(decimal value, bool expected)
    {
        Assert.Equal(expected, value.IsPositive());
    }

    [Theory]
    [InlineData(-1.5, true)]
    [InlineData(-0.001, true)]
    [InlineData(0, false)]
    [InlineData(1.5, false)]
    public void IsNegative_ReturnsExpected(decimal value, bool expected)
    {
        Assert.Equal(expected, value.IsNegative());
    }
}
