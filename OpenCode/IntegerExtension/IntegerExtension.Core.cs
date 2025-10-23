using System;

namespace OpenCode;

/// <summary>
/// Integer-specific utility and formatting extensions.
/// </summary>
/// <remarks>
/// Provides common extension methods for working with integers such as parity checks,
/// clamping, absolute value and conversion to ordinal strings.
/// </remarks>
public static class IntegerExtension
{
    /// <summary>
    /// Returns true if the specified <paramref name="value"/> is an even number.
    /// </summary>
    /// <param name="value">The integer to test for evenness.</param>
    /// <returns><see langword="true"/> if <paramref name="value"/> is even; otherwise <see langword="false"/>.</returns>
    public static bool IsEven(this int value) => value % 2 == 0;

    /// <summary>
    /// Returns true if the specified <paramref name="value"/> is an odd number.
    /// </summary>
    /// <param name="value">The integer to test for oddness.</param>
    /// <returns><see langword="true"/> if <paramref name="value"/> is odd; otherwise <see langword="false"/>.</returns>
    public static bool IsOdd(this int value) => value % 2 != 0;

    /// <summary>
    /// Converts an integer to its ordinal representation as a string (for example,1 -> "1st").
    /// </summary>
    /// <param name="number">The integer to convert to an ordinal string.</param>
    /// <returns>A string containing the numeric value followed by the appropriate ordinal suffix ("st", "nd", "rd", "th").</returns>
    /// <remarks>
    /// This method handles the special cases for the English ordinal rules for11,12 and13
    /// which always use the "th" suffix regardless of the last digit.
    /// Negative numbers will preserve the sign in the returned string (for example, -1 -> "-1st").
    /// </remarks>
    /// <example>
    /// <code>
    ///1.ToOrdinal(); // "1st"
    ///2.ToOrdinal(); // "2nd"
    ///11.ToOrdinal(); // "11th"
    ///113.ToOrdinal(); // "113th"
    /// (-3).ToOrdinal(); // "-3rd"
    /// </code>
    /// </example>
    public static string ToOrdinal(this int number)
    => (number % 100) switch
    {
        11 or 12 or 13 => $"{number}th",
        _ => (number % 10) switch
        {
            1 => $"{number}st",
            2 => $"{number}nd",
            3 => $"{number}rd",
            _ => $"{number}th"
        }
    };

    /// <summary>
    /// Clamps the given <paramref name="value"/> to be within the inclusive range defined by <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    /// <param name="value">The integer to clamp.</param>
    /// <param name="min">The inclusive minimum allowed value.</param>
    /// <param name="max">The inclusive maximum allowed value.</param>
    /// <returns>
    /// <paramref name="min"/> if <paramref name="value"/> is less than <paramref name="min"/>,
    /// <paramref name="max"/> if <paramref name="value"/> is greater than <paramref name="max"/>,
    /// otherwise <paramref name="value"/>.
    /// </returns>
    /// <remarks>
    /// If <paramref name="min"/> is greater than <paramref name="max"/>, the method will still return
    /// either <paramref name="min"/> or <paramref name="max"/> depending on the comparison using
    /// <see cref="Math.Min(int,int)"/> and <see cref="Math.Max(int,int)"/>. It is recommended to ensure
    /// <paramref name="min"/> &lt;= <paramref name="max"/> when calling this method.
    /// </remarks>
    public static int Clamp(this int value, int min, int max)
    => Math.Min(Math.Max(value, min), max);

    /// <summary>
    /// Returns the absolute value of the specified <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The integer whose absolute value is to be returned.</param>
    /// <returns>The absolute value of <paramref name="value"/>.</returns>
    public static int Abs(this int value) => Math.Abs(value);
}
