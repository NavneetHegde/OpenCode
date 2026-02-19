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

    [Theory]
    [InlineData(DayOfWeek.Monday, true)]
    [InlineData(DayOfWeek.Friday, true)]
    [InlineData(DayOfWeek.Saturday, false)]
    [InlineData(DayOfWeek.Sunday, false)]
    public void IsWeekday_ReturnsCorrectResult(DayOfWeek dayOfWeek, bool expected)
    {
        var date = new DateTime(2025, 10, 20).AddDays((int)dayOfWeek - (int)DayOfWeek.Monday);
        Assert.Equal(expected, date.IsWeekday());
    }

    [Fact]
    public void IsBetween_WithinRange_ReturnsTrue()
    {
        var date = new DateTime(2025, 6, 15);
        var start = new DateTime(2025, 1, 1);
        var end = new DateTime(2025, 12, 31);
        Assert.True(date.IsBetween(start, end));
    }

    [Fact]
    public void IsBetween_OutsideRange_ReturnsFalse()
    {
        var date = new DateTime(2026, 1, 1);
        var start = new DateTime(2025, 1, 1);
        var end = new DateTime(2025, 12, 31);
        Assert.False(date.IsBetween(start, end));
    }

    [Fact]
    public void IsBetween_OnBoundary_ReturnsTrue()
    {
        var start = new DateTime(2025, 1, 1);
        Assert.True(start.IsBetween(start, new DateTime(2025, 12, 31)));
    }

    [Fact]
    public void ToUnixTimestamp_ReturnsExpected()
    {
        var date = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var result = date.ToUnixTimestamp();
        Assert.Equal(1735689600, result);
    }

    [Fact]
    public void StartOfMonth_ReturnsFirstDay()
    {
        var date = new DateTime(2025, 10, 21, 15, 30, 45);
        var result = date.StartOfMonth();
        Assert.Equal(new DateTime(2025, 10, 1), result);
    }

    [Fact]
    public void EndOfMonth_ReturnsLastDay()
    {
        var date = new DateTime(2025, 10, 15);
        var result = date.EndOfMonth();
        Assert.Equal(new DateTime(2025, 10, 31, 23, 59, 59, 999), result);
    }

    [Fact]
    public void EndOfMonth_February_LeapYear()
    {
        var date = new DateTime(2024, 2, 10);
        var result = date.EndOfMonth();
        Assert.Equal(29, result.Day);
    }

    [Fact]
    public void StartOfWeek_Monday_ReturnsMonday()
    {
        // 2025-10-23 is a Thursday
        var date = new DateTime(2025, 10, 23);
        var result = date.StartOfWeek(DayOfWeek.Monday);
        Assert.Equal(new DateTime(2025, 10, 20), result);
        Assert.Equal(DayOfWeek.Monday, result.DayOfWeek);
    }

    [Fact]
    public void StartOfWeek_Sunday_ReturnsSunday()
    {
        // 2025-10-23 is a Thursday
        var date = new DateTime(2025, 10, 23);
        var result = date.StartOfWeek(DayOfWeek.Sunday);
        Assert.Equal(new DateTime(2025, 10, 19), result);
        Assert.Equal(DayOfWeek.Sunday, result.DayOfWeek);
    }

    [Theory]
    [InlineData(2025, 2, 28)]
    [InlineData(2024, 2, 29)]
    [InlineData(2025, 1, 31)]
    [InlineData(2025, 4, 30)]
    public void DaysInMonth_ReturnsExpected(int year, int month, int expected)
    {
        var date = new DateTime(year, month, 1);
        Assert.Equal(expected, date.DaysInMonth());
    }
}
