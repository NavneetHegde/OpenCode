using Xunit;

namespace OpenCode.Tests
{
    public partial class StringExtension
    {
        #region ParseInteger
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("abcd!@#$")]
        public void ParseToInteger_ShouldReturnDefault0(string input)
        {
            var result = input.ParseToInt();

            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("abcd!@#$1234")]
        public void ParseToInteger_ShouldReturnDefault5(string input)
        {
            var result = input.ParseToInt(5);

            Assert.Equal(5, result);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("012")]
        public void ParseToInteger_ShouldParseInput(string input)
        {
            var result = input.ParseToInt(5);

            Assert.Equal(int.Parse(input), result);
        }

        #endregion
    }
}
