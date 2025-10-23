using System;
using Xunit;

namespace OpenCode.Tests;

public partial class StringExtensionTests
{
    #region --- ParseToGuid ---

    [Fact]
    public void ParseToGuid_ValidGuid_ReturnsGuid()
    {
        var guid = Guid.NewGuid();
        var str = guid.ToString();
        var result = str.ParseToGuid();
        Assert.Equal(guid, result);
    }

    [Fact]
    public void ParseToGuid_InvalidGuid_ReturnsDefault()
    {
        string? str = "invalid";
        var defaultGuid = Guid.NewGuid();
        var result = str.ParseToGuid(defaultGuid);
        Assert.Equal(defaultGuid, result);
    }

    [Fact]
    public void ParseToGuid_Null_ReturnsDefault()
    {
        string? str = null;
        var defaultGuid = Guid.NewGuid();
        var result = str.ParseToGuid(defaultGuid);
        Assert.Equal(defaultGuid, result);
    }

    #endregion

    #region --- IsGuid ---

    [Fact]
    public void IsGuid_ValidGuid_ReturnsTrue()
    {
        var guid = Guid.NewGuid().ToString();
        Assert.True(guid.IsGuid());
    }

    [Fact]
    public void IsGuid_InvalidGuid_ReturnsFalse()
    {
        string str = "invalid-guid";
        Assert.False(str.IsGuid());
    }

    [Fact]
    public void IsGuid_Null_ReturnsFalse()
    {
        string? str = null;
        Assert.False(str.IsGuid());
    }

    #endregion

    #region --- IsValidGuid ---

    [Fact]
    public void IsValidGuid_ValidNonEmptyGuid_ReturnsTrue()
    {
        var guid = Guid.NewGuid().ToString();
        Assert.True(guid.IsValidGuid());
    }

    [Fact]
    public void IsValidGuid_EmptyGuid_ReturnsFalse()
    {
        var guid = Guid.Empty.ToString();
        Assert.False(guid.IsValidGuid());
    }

    [Fact]
    public void IsValidGuid_InvalidGuid_ReturnsFalse()
    {
        string str = "invalid";
        Assert.False(str.IsValidGuid());
    }

    [Fact]
    public void IsValidGuid_Null_ReturnsFalse()
    {
        string? str = null;
        Assert.False(str.IsValidGuid());
    }

    #endregion
}
