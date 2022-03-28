using System;
using System.Collections.Generic;
using System.IO;
using BrackeysBot.API.Configuration;
using BrackeysBot.API.Permissions;
using NLog;

namespace BrackeysBot.API.Plugins;

/// <summary>
///     Represents a bot plugin.
/// </summary>
public interface IPlugin : IDisposable, IConfigurationHolder
{
    /// <summary>
    ///     Gets the data directory for this plugin.
    /// </summary>
    /// <value>The data directory.</value>
    DirectoryInfo DataDirectory { get; }

    /// <summary>
    ///     Gets the loaded dependencies for this plugin.
    /// </summary>
    /// <value>The plugin dependencies.</value>
    IReadOnlyList<IPlugin> Dependencies { get; }

    /// <summary>
    ///     Gets the loaded dependants for this plugin.
    /// </summary>
    /// <value>The plugin dependants.</value>
    IReadOnlyList<IPlugin> Dependants { get; }

    /// <summary>
    ///     Gets the date and time at which this plugin was last enabled.
    /// </summary>
    /// <value>
    ///     A <see cref="DateTimeOffset" /> representing the date and time at which this plugin was enabled, or
    ///     <see langword="null" /> if this plugin is not currently enabled.
    /// </value>
    DateTimeOffset? EnableTime { get; }

    /// <summary>
    ///     Gets the logger for this plugin.
    /// </summary>
    /// <value>The plugin's logger.</value>
    ILogger Logger { get; }

    /// <summary>
    ///     Gets a list of the default permissions for this plugin.
    /// </summary>
    /// <value>The default permissions.</value>
    IReadOnlyList<Permission> PermissionDefaults { get; }

    /// <summary>
    ///     Gets a list of the current permissions for this plugin.
    /// </summary>
    /// <value>The current permissions.</value>
    IReadOnlyList<Permission> Permissions { get; }

    /// <summary>
    ///     Gets the information about this plugin.
    /// </summary>
    /// <value>A <see cref="BrackeysBot.API.Plugins.PluginInfo" /> object containing</value>
    PluginInfo PluginInfo { get; }

    /// <summary>
    ///     Gets the manager which owns this plugin.
    /// </summary>
    /// <value>The plugin manager.</value>
    IPluginManager PluginManager { get; }

    /// <summary>
    ///     Gets the service provider for this plugin.
    /// </summary>
    /// <value>The service provider.</value>
    public IServiceProvider ServiceProvider { get; }

    /// <summary>
    ///     Gets a permission by its name.
    /// </summary>
    /// <param name="name">The name of the permission.</param>
    /// <returns>The permission with the specified name, or <see langword="null" /> if no matching permission was found.</returns>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="name" /> is <see langword="null" />, empty, or consists of only whitespace.
    /// </exception>
    Permission? GetPermission(string name);
}
