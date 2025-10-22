namespace OpenCode;

/// <summary>
/// Provides extension methods for <see cref="bool"/> values to simplify common conversions
/// and textual representations used throughout the codebase.
/// </summary>
/// <remarks>
/// All methods are pure and have no side effects. They operate on the boolean instance
/// and return small value/type conversions suitable for UI display and simple logic.
/// </remarks>
public static class BoolExtension
{
    /// <summary>
    /// Converts the boolean value to a human-friendly "Yes" or "No" string.
    /// </summary>
    /// <param name="value">The boolean instance to convert.</param>
    /// <returns>
    /// The string "Yes" when <paramref name="value"/> is <see langword="true"/>, otherwise "No".
    /// </returns>
    /// <example>
    /// true.ToYesNo(); // returns "Yes"
    /// false.ToYesNo(); // returns "No"
    /// </example>
    public static string ToYesNo(this bool value) => value ? "Yes" : "No";

    /// <summary>
    /// Returns the capitalized boolean literal "True" or "False".
    /// </summary>
    /// <param name="value">The boolean instance to convert.</param>
    /// <returns>
    /// "True" when <paramref name="value"/> is <see langword="true"/>, otherwise "False".
    /// </returns>
    /// <example>
    /// true.ToTitleCase(); // returns "True"
    /// false.ToTitleCase(); // returns "False"
    /// </example>
    public static string ToTitleCase(this bool value) => value ? "True" : "False";

    /// <summary>
    /// Converts the boolean value to its integer representation.
    /// </summary>
    /// <param name="value">The boolean instance to convert.</param>
    /// <returns>
    /// An integer where <see langword="true"/> maps to <c>1</c> and <see langword="false"/> maps to <c>0</c>.
    /// </returns>
    /// <example>
    /// true.ToInt(); // returns 1
    /// false.ToInt(); // returns 0
    /// </example>
    public static int ToInt(this bool value) => value ? 1 : 0;

    /// <summary>
    /// Returns the lowercase string representation of the boolean ("true" or "false").
    /// </summary>
    /// <param name="value">The boolean instance to convert.</param>
    /// <returns>
    /// The lowercase invariant string "true" when <paramref name="value"/> is <see langword="true"/>,
    /// otherwise "false".
    /// </returns>
    /// <remarks>
    /// Uses <see cref="System.String.ToLowerInvariant"/> to ensure a culture-invariant
    /// lowercase result.
    /// </remarks>
    /// <example>
    /// true.ToLowerString(); // returns "true"
    /// false.ToLowerString(); // returns "false"
    /// </example>
    public static string ToLowerString(this bool value) => value.ToString().ToLowerInvariant();
}
