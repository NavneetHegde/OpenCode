using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace OpenCode.Tests
{
    public partial class StringExtension
    {
        #region RemoveTrailingZero
        [Theory]
        [InlineData("2")]
        [InlineData("2.000")]
        [InlineData("02")]
        public void RemoveTrailingZero_ShouldReturn2(string input)
        {
            var result = input.RemoveTrailingZero();

            Assert.Equal("2", result);
        }

        [Theory]
        [InlineData("2.01")]
        [InlineData("2.0100")]
        [InlineData("02.010")]
        public void RemoveTrailingZero_ShouldReturn201(string input)
        {
            var result = input.RemoveTrailingZero();

            Assert.Equal("2.01", result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("test")]
        [InlineData("02v.0")]
        [InlineData("2")]
        public void RemoveTrailingZero_ShouldReturnIuputAsIs(string input)
        {
            var result = input.RemoveTrailingZero();

            Assert.Equal(input, result);
        }
        #endregion

        #region SHA256 Hash
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SHA256tHash_ShouldReturnHashFailed(string input)
        {
            bool hashed;
            var result = input.SHA256tHash(out hashed);

            Assert.False(hashed);
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void SHA256tHash_ValidInput_ReturnsHashAndSetsHasHashedTrue()
        {
            // Arrange
            string input = "test";
            bool hasHashed;

            // Act
            var result = input.SHA256tHash(out hasHashed);

            // Assert
            Assert.NotEmpty(result);
            Assert.True(hasHashed);

            // To further validate, we can compute the expected hash:
            using (var sha256 = SHA256.Create())
            {
                var expectedHashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var expectedHash = Convert.ToBase64String(expectedHashBytes);
                Assert.Equal(expectedHash, result);
            }
        }

        [Fact]
        public void SHA256tHash_SameInput_ReturnsSameHash()
        {
            // Arrange
            string input = "consistent";
            bool hasHashed1, hasHashed2;

            // Act
            var result1 = input.SHA256tHash(out hasHashed1);
            var result2 = input.SHA256tHash(out hasHashed2);

            // Assert
            Assert.Equal(result1, result2);
            Assert.True(hasHashed1);
            Assert.True(hasHashed2);
        }

        #endregion
    }
}
