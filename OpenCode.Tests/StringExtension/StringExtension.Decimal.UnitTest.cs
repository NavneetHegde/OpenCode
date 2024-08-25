using Xunit;

namespace OpenCode.Tests
{
    public partial class StringExtension
    {
        #region ParseDecimal
        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData("abcd!@#$", 0)]
        public void ParseToDecimal_ShouldReturnDefault0(string input, decimal expected)
        {
            var result = input.ParseToDecimal();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null, 5)]
        [InlineData("", 5)]
        [InlineData("abcd!@#$1234", 5)]
        public void ParseToDecimal_ShouldReturnDefault5(string input, decimal expected)
        {
            var result = input.ParseToDecimal(expected);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1", 5)]
        [InlineData("1.0", 5)]
        [InlineData("01.0", 5)]
        public void ParseToDecimal_ShouldParseInput(string input, decimal expected)
        {
            var result = input.ParseToDecimal(expected);

            Assert.Equal(decimal.Parse(input), result);
        }

        #endregion
    }
}
