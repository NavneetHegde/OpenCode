using System;
using System.Globalization;

namespace OpenCode;

/// <summary>
/// Decimal value rounding, formatting, and comparison helpers.
/// </summary>
public static class DecimalExtension
{
    /// <summary>
    /// Rounds a decimal to specified number of decimal places.
    /// </summary>
    public static decimal RoundTo(this decimal value, int decimals)
        => Math.Round(value, decimals, MidpointRounding.AwayFromZero);

    /// <summary>
    /// Returns formatted decimal as string with thousands separator.
    /// </summary>
    public static string ToMoneyFormat(this decimal value, string format = "N2", CultureInfo? culture = null)
        => value.ToString(format, culture ?? CultureInfo.InvariantCulture);

    /// <summary>
    /// Checks if two decimals are approximately equal within a tolerance.
    /// </summary>
    public static bool AlmostEquals(this decimal a, decimal b, decimal tolerance = 0.0001m)
        => Math.Abs(a - b) <= tolerance;

    /// <summary>
    /// Converts decimal to percentage string (e.g., 0.25 → "25%").
    /// </summary>
    public static string ToPercentage(this decimal value, int decimals = 0)
        => (value * 100).ToString($"F{decimals}", CultureInfo.InvariantCulture) + "%";

    /// <summary>
    /// Converts decimal to currency string (e.g., "$12.34").
    /// </summary>
    public static string ToCurrency(this decimal value, CultureInfo? culture = null)
        => string.Format(culture ?? CultureInfo.CurrentCulture, "{0:C}", value);
}
