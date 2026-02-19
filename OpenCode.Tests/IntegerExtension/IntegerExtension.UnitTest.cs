using Xunit;

namespace OpenCode.Tests;

public class IntegerExtensionTests
{
    [Theory]
    [InlineData(2, true)]
    [InlineData(4, true)]
    [InlineData(1, false)]
    [InlineData(-2, true)]
    [InlineData(-3, false)]
    public void IsEven_ReturnsExpectedResult(int value, bool expected)
    {
        Assert.Equal(expected, value.IsEven());
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(3, true)]
    [InlineData(2, false)]
    [InlineData(-1, true)]
    [InlineData(-4, false)]
    public void IsOdd_ReturnsExpectedResult(int value, bool expected)
    {
        Assert.Equal(expected, value.IsOdd());
    }

    [Theory]
    [InlineData(1, "1st")]
    [InlineData(2, "2nd")]
    [InlineData(3, "3rd")]
    [InlineData(4, "4th")]
    [InlineData(11, "11th")]
    [InlineData(12, "12th")]
    [InlineData(13, "13th")]
    [InlineData(21, "21st")]
    [InlineData(22, "22nd")]
    [InlineData(23, "23rd")]
    [InlineData(101, "101st")]
    public void ToOrdinal_ReturnsExpectedString(int number, string expected)
    {
        Assert.Equal(expected, number.ToOrdinal());
    }

    [Theory]
    [InlineData(5, 1, 10, 5)]
    [InlineData(0, 1, 10, 1)]
    [InlineData(15, 1, 10, 10)]
    [InlineData(-5, -10, 0, -5)]
    [InlineData(-15, -10, 0, -10)]
    public void Clamp_ReturnsValueWithinRange(int value, int min, int max, int expected)
    {
        Assert.Equal(expected, value.Clamp(min, max));
    }

    [Theory]
    [InlineData(5, 5)]
    [InlineData(-5, 5)]
    [InlineData(0, 0)]
    [InlineData(-123, 123)]
    public void Abs_ReturnsAbsoluteValue(int value, int expected)
    {
        Assert.Equal(expected, value.Abs());
    }

    [Theory]
    [InlineData(5, 1, 10, true)]
    [InlineData(1, 1, 10, true)]
    [InlineData(10, 1, 10, true)]
    [InlineData(0, 1, 10, false)]
    [InlineData(11, 1, 10, false)]
    public void IsBetween_ReturnsExpected(int value, int min, int max, bool expected)
    {
        Assert.Equal(expected, value.IsBetween(min, max));
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(100, true)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    public void IsPositive_ReturnsExpected(int value, bool expected)
    {
        Assert.Equal(expected, value.IsPositive());
    }

    [Theory]
    [InlineData(-1, true)]
    [InlineData(-100, true)]
    [InlineData(0, false)]
    [InlineData(1, false)]
    public void IsNegative_ReturnsExpected(int value, bool expected)
    {
        Assert.Equal(expected, value.IsNegative());
    }
}
