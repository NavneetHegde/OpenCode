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

        [Theory]
        [InlineData("TestMyHash")]
        public void SHA256tHash_ShouldReturnValidHash(string input)
        {
            bool hashed;
            var result = input.SHA256tHash(out hashed);

            Assert.True(hashed);
            Assert.Equal("nP45Z0r63tlUmXg4QVoh+GhiFkES1xhdY57UDTX99r8=", result);
            Assert.Equal(44, result.Length);
        }

        #endregion

    }
}
