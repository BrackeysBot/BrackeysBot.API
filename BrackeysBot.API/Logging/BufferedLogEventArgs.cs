using System;
using System.Collections.Generic;

namespace BrackeysBot.API.Logging;

/// <summary>
///     Provides event information for <see cref="IBotApplication.BufferedLog" />.
/// </summary>
public sealed class BufferedLogEventArgs : EventArgs
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="BufferedLogEventArgs" /> class.
    /// </summary>
    /// <param name="logEvents">The buffered log events.</param>
    public BufferedLogEventArgs(IEnumerable<string> logEvents)
    {
        LogEvents = logEvents;
    }

    /// <summary>
    ///     Gets the enumerable collection containing the buffered log events.
    /// </summary>
    /// <value>The buffered log events.</value>
    public IEnumerable<string> LogEvents { get; }
}
