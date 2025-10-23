using System;
using Xunit;


namespace OpenCode.Tests;

public partial class StringExtensionDateTimeTests
{
    #region --- ParseToDateTime ---

    [Theory]
    [InlineData("2025-10-21", 2025, 10, 21)]
    [InlineData("10/21/2025", 2025, 10, 21)]
    [InlineData(null, 1, 1, 2000)] // default value used
    [InlineData("invalid", 1, 1, 2000)] // default value used
    public void ParseToDateTime_ReturnsExpected(string? input, int expectedYear, int expectedMonth, int expectedDay)
    {
        var defaultValue = new DateTime(2000, 1, 1);
        var result = input.ParseToDateTime(defaultValue);

        if (input == null || input == "invalid")
        {
            Assert.Equal(defaultValue, result);
        }
        else
        {
            Assert.Equal(expectedYear, result.Year);
            Assert.Equal(expectedMonth, result.Month);
            Assert.Equal(expectedDay, result.Day);
        }
    }

    #endregion

    #region --- IsDateTime ---

    [Theory]
    [InlineData("2025-10-21", true)]
    [InlineData("10/21/2025", true)]
    [InlineData("invalid", false)]
    [InlineData(null, false)]
    public void IsDateTime_ReturnsExpected(string? input, bool expected)
    {
        Assert.Equal(expected, input.IsDateTime());
    }

    #endregion

    #region --- ToFormattedDate ---

    [Theory]
    [InlineData("2025-10-21", "yyyy/MM/dd", "2025/10/21")]
    [InlineData("10/21/2025", "MM-dd-yyyy", "10-21-2025")]
    [InlineData("invalid", "yyyy-MM-dd", "invalid")]
    [InlineData(null, "yyyy-MM-dd", "")]
    public void ToFormattedDate_ReturnsExpected(string? input, string format, string? expected)
    {
        Assert.Equal(expected, input.ToFormattedDate(format));
    }

    #endregion

    #region --- ParseToDateOnly ---

    [Theory]
    [InlineData("2025-10-21", 2025, 10, 21)]
    [InlineData("10/21/2025", 2025, 10, 21)]
    [InlineData(null, 2000, 1, 1)]
    [InlineData("invalid", 2000, 1, 1)]
    public void ParseToDateOnly_ReturnsExpected(string? input, int expectedYear, int expectedMonth, int expectedDay)
    {
        var defaultValue = new DateOnly(2000, 1, 1);
        var result = input.ParseToDateOnly(defaultValue);

        if (string.IsNullOrWhiteSpace(input) || input == "invalid")
        {
            Assert.Equal(defaultValue, result);
        }
        else
        {
            Assert.Equal(expectedYear, result.Year);
            Assert.Equal(expectedMonth, result.Month);
            Assert.Equal(expectedDay, result.Day);
        }
    }

    #endregion
}
