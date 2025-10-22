using System;
using System.Globalization;

namespace OpenCode;

/// <summary>
/// DateTime formatting and calculation extension methods.
/// </summary>
/// <remarks>
/// All methods operate on <see cref="DateTime"/> instances and return values in the
/// same <see cref="DateTime.Kind"/> when applicable. Consumers should be aware of
/// timezone/kind semantics when passing values (UTC vs Local).
/// </remarks>
public static class DateTimeExtension
{
    /// <summary>
    /// Converts the specified <see cref="DateTime"/> to a string using the provided
    /// .NET format string and the invariant culture.
    /// </summary>
    /// <param name="date">The <see cref="DateTime"/> to format.</param>
    /// <param name="format">
    /// A standard or custom date and time format string. Defaults to
    /// <c>"yyyy-MM-dd HH:mm:ss"</c> when not provided.
    /// </param>
    /// <returns>
    /// The formatted date/time string produced by calling
    /// <see cref="DateTime.ToString(string, IFormatProvider)"/> with
    /// <see cref="CultureInfo.InvariantCulture"/>.
    /// </returns>
    public static string ToFormat(this DateTime date, string format = "yyyy-MM-dd HH:mm:ss")
        => date.ToString(format, CultureInfo.InvariantCulture);

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> falls on a weekend.
    /// </summary>
    /// <param name="date">The date to evaluate.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="date"/> is a Saturday or Sunday; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsWeekend(this DateTime date)
        => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    /// <summary>
    /// Determines whether the specified <see cref="DateTime"/> represents today's date.
    /// </summary>
    /// <param name="date">The date to evaluate.</param>
    /// <returns>
    /// <c>true</c> if the date portion of <paramref name="date"/> is equal to
    /// <see cref="DateTime.Today"/>; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// Comparison uses the date component only (time-of-day is ignored).
    /// </remarks>
    public static bool IsToday(this DateTime date)
        => date.Date == DateTime.Today;

    /// <summary>
    /// Gets the start of the day for the specified <see cref="DateTime"/> (time set to midnight).
    /// </summary>
    /// <param name="date">The date for which to obtain the start-of-day.</param>
    /// <returns>
    /// A <see cref="DateTime"/> equal to the date portion of <paramref name="date"/> with
    /// the time component set to 00:00:00. The returned value preserves the original
    /// <see cref="DateTime.Kind"/>.
    /// </returns>
    public static DateTime StartOfDay(this DateTime date)
        => date.Date;

    /// <summary>
    /// Gets the end of the day for the specified <see cref="DateTime"/>.
    /// </summary>
    /// <param name="date">The date for which to obtain the end-of-day.</param>
    /// <returns>
    /// A <see cref="DateTime"/> representing the last millisecond of the day
    /// (typically 23:59:59.999) for the date portion of <paramref name="date"/>.
    /// </returns>
    /// <remarks>
    /// This implementation subtracts one millisecond from the next day's midnight.
    /// Note that <see cref="DateTime"/> precision is limited to ticks; for scenarios
    /// that require higher precision or exact interval semantics consider using
    /// half-open intervals like [StartOfDay, StartOfNextDay).
    /// </remarks>
    public static DateTime EndOfDay(this DateTime date)
        => date.Date.AddDays(1).AddMilliseconds(-1);

    /// <summary>
    /// Calculates the age in whole years from the specified birth date to today.
    /// </summary>
    /// <param name="birthDate">The birth date to calculate the age from.</param>
    /// <returns>
    /// The number of full years elapsed since <paramref name="birthDate"/> up to
    /// <see cref="DateTime.Today"/>. Returns 0 for birth dates on or after today.
    /// </returns>
    /// <remarks>
    /// Calculation uses the current date (<see cref="DateTime.Today"/>) and accounts
    /// for whether the birthday has occurred this year. This method ignores time-of-day.
    /// </remarks>
    public static int ToAge(this DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age;
    }
}
