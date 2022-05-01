namespace BrackeysBot.API;

/// <summary>
///     An enumeration of the supported markdown timestamp formats.
/// </summary>
public enum TimestampFormat
{
    /// <summary>
    ///     Example: <c>17:32</c>
    /// </summary>
    ShortTime = 't',

    /// <summary>
    ///     Example: <c>17:32:00</c>
    /// </summary>
    LongTime = 'T',

    /// <summary>
    ///     Example: <c>01/05/2022</c>
    /// </summary>
    ShortDate = 'd',

    /// <summary>
    ///     Example: <c>1 May 2022</c>
    /// </summary>
    LongDate = 'D',

    /// <summary>
    ///     Example: <c>1 May 2022 at 17:32</c>
    /// </summary>
    LongDateShortTime = 'f',

    /// <summary>
    ///     Example: <c>Sunday, 1 May 2022 at 17:32</c>
    /// </summary>
    LongDateWithDayOfWeekShortTime = 'F',

    /// <summary>
    ///     Example: <c>4 hours ago</c>
    /// </summary>
    Relative = 'R'
}
