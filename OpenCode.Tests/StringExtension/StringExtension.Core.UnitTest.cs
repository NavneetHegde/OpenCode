using Xunit;

namespace OpenCode.Tests;

public partial class StringExtensionTests
{
    #region --- Core Helpers ---

    [Theory]
    [InlineData(null, null)]
    [InlineData("", null)]
    [InlineData("   ", null)]
    [InlineData("abc", "abc")]
    public void NullIfEmpty_ReturnsExpected(string? input, string? expected)
    {
        Assert.Equal(expected, input.NullIfEmpty());
    }

    [Theory]
    [InlineData(null, "default", "default")]
    [InlineData("", "default", "default")]
    [InlineData("abc", "default", "abc")]
    public void OrDefault_ReturnsExpected(string? input, string defaultValue, string? expected)
    {
        Assert.Equal(expected, input.OrDefault(defaultValue));
    }

    [Theory]
    [InlineData(null, "")]
    [InlineData("  abc  ", "abc")]
    [InlineData("abc", "abc")]
    public void SafeTrim_ReturnsExpected(string? input, string? expected)
    {
        Assert.Equal(expected, input.SafeTrim());
    }

    [Theory]
    [InlineData(null, 3, "")]
    [InlineData("abc", 2, "ab")]
    [InlineData("abc", 5, "abc")]
    public void Truncate_ReturnsExpected(string? input, int maxLength, string? expected)
    {
        Assert.Equal(expected, input.Truncate(maxLength));
    }

    [Theory]
    [InlineData("123", true)]
    [InlineData("12.34", true)]
    [InlineData("abc", false)]
    [InlineData(null, false)]
    public void IsNumeric_ReturnsExpected(string? input, bool expected)
    {
        Assert.Equal(expected, input.IsNumeric());
    }

    [Theory]
    [InlineData("abc", "ABC", true)]
    [InlineData("abc", "Abd", false)]
    [InlineData(null, null, true)]
    [InlineData(null, "abc", false)]
    public void EqualsIgnoreCase_ReturnsExpected(string? input, string? other, bool expected)
    {
        Assert.Equal(expected, input.EqualsIgnoreCase(other));
    }

    #endregion

    #region --- Format Helpers ---

    [Theory]
    [InlineData("hello world", "Hello World")]
    [InlineData(null, "")]
    public void ToTitleCase_ReturnsExpected(string? input, string? expected)
    {
        Assert.Equal(expected, input.ToTitleCase());
    }

    [Theory]
    [InlineData("hello world", "HelloWorld")]
    [InlineData("  multiple_words-here  ", "MultipleWordsHere")]
    [InlineData(null, "")]
    public void ToPascalCase_ReturnsExpected(string? input, string? expected)
    {
        Assert.Equal(expected, input.ToPascalCase());
    }

    [Theory]
    [InlineData("hello world", "helloWorld")]
    [InlineData("  multiple_words-here  ", "multipleWordsHere")]
    [InlineData(null, "")]
    public void ToCamelCase_ReturnsExpected(string? input, string? expected)
    {
        Assert.Equal(expected, input.ToCamelCase());
    }

    [Theory]
    [InlineData("helloWorldTest", "hello_world_test")]
    [InlineData("Multiple Words-Here", "multiple_words_here")]
    [InlineData(null, "")]
    public void ToSnakeCase_ReturnsExpected(string? input, string? expected)
    {
        Assert.Equal(expected, input.ToSnakeCase());
    }

    [Theory]
    [InlineData("helloWorldTest", "hello-world-test")]
    [InlineData("Multiple Words_Here", "multiple-words-here")]
    [InlineData(null, "")]
    public void ToKebabCase_ReturnsExpected(string? input, string? expected)
    {
        Assert.Equal(expected, input.ToKebabCase());
    }

    [Theory]
    [InlineData("abcdef", 1, 1, '*', "a****f")]
    [InlineData("abc", 1, 1, '*', "a*c")]
    [InlineData(null, 1, 1, '*', "")]
    public void Mask_ReturnsExpected(string? input, int start, int end, char maskChar, string? expected)
    {
        Assert.Equal(expected, input.Mask(start, end, maskChar));
    }

    [Fact]
    public void FormatWith_ReturnsFormattedString()
    {
        string template = "Hello {0}, today is {1}";
        string result = template.FormatWith("Alice", "Monday");
        Assert.Equal("Hello Alice, today is Monday", result);
    }

    #endregion

    #region --- Utility Helpers ---

    [Theory]
    [InlineData("abcdef", 1, 3, "bcd")]
    [InlineData("abcdef", -1, 3, "abc")]
    [InlineData("abcdef", 10, 3, "")]
    [InlineData(null, 0, 3, "")]
    public void SafeSubstring_ReturnsExpected(string? input, int start, int length, string? expected)
    {
        Assert.Equal(expected, input.SafeSubstring(start, length));
    }

    [Theory]
    [InlineData("café", "cafe")]
    [InlineData("àéîöü", "aeiou")]
    [InlineData(null, "")]
    public void RemoveDiacritics_ReturnsExpected(string? input, string? expected)
    {
        Assert.Equal(expected, input.RemoveDiacritics());
    }

    [Theory]
    [InlineData("abc123!@#", "abc123")]
    [InlineData(null, "")]
    public void RemoveNonAlphanumeric_ReturnsExpected(string? input, string? expected)
    {
        Assert.Equal(expected, input.RemoveNonAlphanumeric());
    }

    [Theory]
    [InlineData("Hello World!", "hello-world")]
    [InlineData("Café au lait", "cafe-au-lait")]
    [InlineData(null, "")]
    public void ToSlug_ReturnsExpected(string? input, string? expected)
    {
        Assert.Equal(expected, input.ToSlug());
    }

    [Theory]
    [InlineData("test@example.com", true)]
    [InlineData("invalid-email", false)]
    [InlineData(null, false)]
    public void IsEmail_ReturnsExpected(string? input, bool expected)
    {
        Assert.Equal(expected, input.IsEmail());
    }

    [Theory]
    [InlineData("{\"key\":\"value\"}", true)]
    [InlineData("[1,2,3]", true)]
    [InlineData("not json", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsJson_ReturnsExpected(string? input, bool expected)
    {
        Assert.Equal(expected, input.IsJson());
    }

    [Theory]
    [InlineData("abc", "cba")]
    [InlineData("hello", "olleh")]
    [InlineData("a", "a")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void Reverse_ReturnsExpected(string? input, string expected)
    {
        Assert.Equal(expected, input.Reverse());
    }

    [Theory]
    [InlineData("Hello World", "world", true)]
    [InlineData("Hello World", "HELLO", true)]
    [InlineData("Hello World", "xyz", false)]
    [InlineData(null, "test", false)]
    [InlineData("test", null, false)]
    public void ContainsIgnoreCase_ReturnsExpected(string? input, string? value, bool expected)
    {
        Assert.Equal(expected, input.ContainsIgnoreCase(value));
    }

    [Theory]
    [InlineData("abcdef", 3, "abc")]
    [InlineData("ab", 5, "ab")]
    [InlineData(null, 3, "")]
    [InlineData("abc", 0, "")]
    public void Left_ReturnsExpected(string? input, int length, string expected)
    {
        Assert.Equal(expected, input.Left(length));
    }

    [Theory]
    [InlineData("abcdef", 3, "def")]
    [InlineData("ab", 5, "ab")]
    [InlineData(null, 3, "")]
    [InlineData("abc", 0, "")]
    public void Right_ReturnsExpected(string? input, int length, string expected)
    {
        Assert.Equal(expected, input.Right(length));
    }

    [Theory]
    [InlineData("hello world", 2)]
    [InlineData("one", 1)]
    [InlineData("  multiple   spaces  here  ", 3)]
    [InlineData(null, 0)]
    [InlineData("", 0)]
    [InlineData("   ", 0)]
    public void WordCount_ReturnsExpected(string? input, int expected)
    {
        Assert.Equal(expected, input.WordCount());
    }

    [Theory]
    [InlineData("world", "/", "/world")]
    [InlineData("/world", "/", "/world")]
    [InlineData(null, "/", "/")]
    [InlineData("", "/", "/")]
    public void EnsureStartsWith_ReturnsExpected(string? input, string prefix, string expected)
    {
        Assert.Equal(expected, input.EnsureStartsWith(prefix));
    }

    [Theory]
    [InlineData("hello", "/", "hello/")]
    [InlineData("hello/", "/", "hello/")]
    [InlineData(null, "/", "/")]
    [InlineData("", "/", "/")]
    public void EnsureEndsWith_ReturnsExpected(string? input, string suffix, string expected)
    {
        Assert.Equal(expected, input.EnsureEndsWith(suffix));
    }

    #endregion
}
