using System;
using BrackeysBot.API.Logging;
using BrackeysBot.API.Plugins;
using NLog;

namespace BrackeysBot.API;

/// <summary>
///     Represents a bot application instance.
/// </summary>
public interface IBotApplication
{
    /// <summary>
    ///     Gets the current bot application instance.
    /// </summary>
    /// <value>The current bot application instance.</value>
    static IBotApplication Current { get; internal set; } = null!;

    /// <summary>
    ///     Gets the bot version.
    /// </summary>
    /// <value>The bot version.</value>
    static string Version { get; } = string.Empty;

    /// <summary>
    ///     Occurs when the buffered log has flushed the log events.
    /// </summary>
    event EventHandler<BufferedLogEventArgs>? BufferedLog;

    /// <summary>
    ///     Gets the logger for this bot.
    /// </summary>
    /// <value>The logger.</value>
    ILogger Logger { get; }

    /// <summary>
    ///     Gets the plugin manager.
    /// </summary>
    /// <value>The plugin manager.</value>
    IPluginManager PluginManager { get; }
}
