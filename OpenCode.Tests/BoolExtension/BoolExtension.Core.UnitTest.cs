using Xunit;

namespace OpenCode.Tests;

public class BoolExtensionTests
{
    [Theory]
    [InlineData(true, "Yes")]
    [InlineData(false, "No")]
    public void ToYesNo_ReturnsExpectedString(bool input, string expected)
    {
        var result = input.ToYesNo();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, "True")]
    [InlineData(false, "False")]
    public void ToTitleCase_ReturnsExpectedString(bool input, string expected)
    {
        var result = input.ToTitleCase();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, 1)]
    [InlineData(false, 0)]
    public void ToInt_ReturnsExpectedInteger(bool input, int expected)
    {
        var result = input.ToInt();
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(true, "true")]
    [InlineData(false, "false")]
    public void ToLowerString_ReturnsExpectedString(bool input, string expected)
    {
        var result = input.ToLowerString();
        Assert.Equal(expected, result);
    }
}
