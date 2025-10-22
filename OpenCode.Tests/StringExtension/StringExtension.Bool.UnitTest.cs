using Xunit;

namespace OpenCode.Tests;

public partial class StringExtensionTests
{
    // ----------------------
    // Tests for ParseToBool
    // ----------------------
    [Theory]
    [InlineData("true", true)]
    [InlineData("True", true)]
    [InlineData(" false ", false)]
    [InlineData("1", true)]
    [InlineData("0", false)]
    [InlineData("yes", true)]
    [InlineData("no", false)]
    [InlineData("y", true)]
    [InlineData("n", false)]
    [InlineData("on", true)]
    [InlineData("off", false)]
    [InlineData("invalid", true)] // defaultValue = true
    [InlineData(null, false)]     // defaultValue = false
    public void ParseToBool_ReturnsExpectedResult(string? input, bool expected)
    {
        var result = input.ParseToBool(defaultValue: expected); // default true for testing invalid cases
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ParseToBool_WithDefaultValueUsed()
    {
        string? input = "maybe";
        bool result = input.ParseToBool(defaultValue: true);
        Assert.True(result);

        result = input.ParseToBool(defaultValue: false);
        Assert.False(result);
    }

    // ----------------------
    // Tests for IsBool
    // ----------------------
    [Theory]
    [InlineData("true", true)]
    [InlineData("false", true)]
    [InlineData("1", true)]
    [InlineData("0", true)]
    [InlineData("yes", true)]
    [InlineData("no", true)]
    [InlineData("y", true)]
    [InlineData("n", true)]
    [InlineData("on", true)]
    [InlineData("off", true)]
    [InlineData("maybe", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsBool_ReturnsExpectedResult(string? input, bool expected)
    {
        Assert.Equal(expected, input.IsBool());
    }

    // ----------------------
    // Tests for ToYesNo
    // ----------------------
    [Theory]
    [InlineData("true", "Yes")]
    [InlineData("false", "No")]
    [InlineData("1", "Yes")]
    [InlineData("0", "No")]
    [InlineData("yes", "Yes")]
    [InlineData("no", "No")]
    [InlineData("on", "Yes")]
    [InlineData("off", "No")]
    [InlineData("maybe", "No")] // defaults to false
    [InlineData(null, "No")]    // defaults to false
    public void ToYesNo_ReturnsExpectedResult(string? input, string expected)
    {
        var result = input.ToYesNo();
        Assert.Equal(expected, result);
    }
}
