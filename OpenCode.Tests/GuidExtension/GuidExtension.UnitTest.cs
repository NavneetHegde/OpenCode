using System;
using Xunit;

namespace OpenCode.Tests;

public class GuidExtensionTests
{
    [Fact]
    public void IsNotEmpty_ReturnsTrueForNonEmptyGuid()
    {
        var guid = Guid.NewGuid();
        Assert.True(guid.IsNotEmpty());
    }

    [Fact]
    public void IsNotEmpty_ReturnsFalseForEmptyGuid()
    {
        var guid = Guid.Empty;
        Assert.False(guid.IsNotEmpty());
    }

    [Fact]
    public void IsEmpty_ReturnsTrueForEmptyGuid()
    {
        var guid = Guid.Empty;
        Assert.True(guid.IsEmpty());
    }

    [Fact]
    public void IsEmpty_ReturnsFalseForNonEmptyGuid()
    {
        var guid = Guid.NewGuid();
        Assert.False(guid.IsEmpty());
    }

    [Fact]
    public void ToCompactString_RemovesHyphens()
    {
        var guid = Guid.Parse("12345678-1234-1234-1234-1234567890ab");
        var result = guid.ToCompactString();
        Assert.Equal("123456781234123412341234567890ab", result);
    }

    [Fact]
    public void ToShortGuid_Returns22CharacterString()
    {
        var guid = Guid.NewGuid();
        var result = guid.ToShortGuid();

        Assert.Equal(22, result.Length);
        // Ensure no '/' or '+' characters
        Assert.DoesNotContain("/", result);
        Assert.DoesNotContain("+", result);
    }

    [Fact]
    public void ToShortGuid_UniqueForDifferentGuids()
    {
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();

        var short1 = guid1.ToShortGuid();
        var short2 = guid2.ToShortGuid();

        Assert.NotEqual(short1, short2);
    }
}
