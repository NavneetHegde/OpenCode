using System;
using Xunit;

namespace OpenCode.Tests;

public class DateTimeExtensionTests
{
    [Theory]
    [InlineData("2025-10-21 15:30:00", "yyyy-MM-dd HH:mm:ss", "2025-10-21 15:30:00")]
    [InlineData("2025-10-21 15:30:00", "MM/dd/yyyy", "10/21/2025")]
    public void ToFormat_ReturnsExpectedString(string dateStr, string format, string expected)
    {
        var date = DateTime.Parse(dateStr);
        var result = date.ToFormat(format);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(DayOfWeek.Monday, false)]
    [InlineData(DayOfWeek.Tuesday, false)]
    [InlineData(DayOfWeek.Wednesday, false)]
    [InlineData(DayOfWeek.Thursday, false)]
    [InlineData(DayOfWeek.Friday, false)]
    [InlineData(DayOfWeek.Saturday, true)]
    [InlineData(DayOfWeek.Sunday, true)]
    public void IsWeekend_ReturnsCorrectResult(DayOfWeek dayOfWeek, bool expected)
    {
        var date = new DateTime(2025, 10, 20).AddDays((int)dayOfWeek - (int)DayOfWeek.Monday);
        var result = date.IsWeekend();
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IsToday_ReturnsTrueForToday()
    {
        var today = DateTime.Today.AddHours(10);
        Assert.True(today.IsToday());
    }

    [Fact]
    public void IsToday_ReturnsFalseForOtherDate()
    {
        var yesterday = DateTime.Today.AddDays(-1);
        Assert.False(yesterday.IsToday());
    }

    [Fact]
    public void StartOfDay_ReturnsMidnight()
    {
        var date = new DateTime(2025, 10, 21, 15, 30, 45);
        var start = date.StartOfDay();
        Assert.Equal(new DateTime(2025, 10, 21, 0, 0, 0), start);
    }

    [Fact]
    public void EndOfDay_ReturnsEndOfDay()
    {
        var date = new DateTime(2025, 10, 21, 15, 30, 45);
        var end = date.EndOfDay();
        Assert.Equal(new DateTime(2025, 10, 21, 23, 59, 59, 999), end);
    }

    [Theory]
    [InlineData("2000-10-21", 25)] // assuming test is run in 2025
    [InlineData("2020-10-21", 5)]
    [InlineData("2025-10-21", 0)]
    public void ToAge_ReturnsExpectedAge(string birthDateStr, int expectedAge)
    {
        var birthDate = DateTime.Parse(birthDateStr);
        var result = birthDate.ToAge();
        Assert.Equal(expectedAge, result);
    }
}
