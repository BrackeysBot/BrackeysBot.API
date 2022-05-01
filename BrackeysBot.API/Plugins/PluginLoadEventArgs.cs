using System;

namespace BrackeysBot.API.Plugins;

/// <summary>
///     Provides event information for <see cref="IPluginManager.PluginLoaded" />.
/// </summary>
public sealed class PluginLoadEventArgs : EventArgs
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PluginLoadEventArgs" /> class.
    /// </summary>
    /// <param name="plugin">The plugin that was loaded.</param>
    public PluginLoadEventArgs(IPlugin plugin)
    {
        Plugin = plugin;
    }

    /// <summary>
    ///     Gets the plugin that was loaded.
    /// </summary>
    /// <value>The loaded plugin.</value>
    public IPlugin Plugin { get; }
}
