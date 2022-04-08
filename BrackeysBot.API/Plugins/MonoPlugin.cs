using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Loader;
using System.Threading.Tasks;
using BrackeysBot.API.Configuration;
using BrackeysBot.API.Permissions;
using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace BrackeysBot.API.Plugins;

/// <summary>
///     Represents a bot plugin.
/// </summary>
public abstract class MonoPlugin : IPlugin
{
    private DiscordClient? _client;

    internal AssemblyLoadContext LoadContext { get; set; } = null!;

    /// <summary>
    ///     Gets the underlying <see cref="DSharpPlus.DiscordClient" /> instance.
    /// </summary>
    /// <value>The underlying <see cref="DSharpPlus.DiscordClient" />.</value>
    /// <exception cref="NotSupportedException">The plugin does not have a bot token associated with it.</exception>
    protected internal DiscordClient DiscordClient
    {
        get
        {
            if (_client is null)
                throw new NotSupportedException("Cannot access the DiscordClient for a plugin which has no token.");

            return _client;
        }
        internal set => _client = value;
    }

    /// <inheritdoc />
    public IConfiguration Configuration { get; internal set; } = null!;

    /// <inheritdoc />
    public DirectoryInfo DataDirectory { get; internal set; } = null!;

    /// <inheritdoc />
    public IReadOnlyList<IPlugin> Dependencies { get; internal set; } = ArraySegment<IPlugin>.Empty;

    /// <inheritdoc />
    public IReadOnlyList<IPlugin> Dependants { get; internal set; } = ArraySegment<IPlugin>.Empty;

    /// <inheritdoc />
    public DateTimeOffset? EnableTime { get; internal set; } = null!;

    /// <inheritdoc />
    public ILogger Logger { get; internal set; } = null!;

    /// <inheritdoc />
    public IReadOnlyList<Permission> PermissionDefaults { get; internal set; } = ArraySegment<Permission>.Empty;

    /// <inheritdoc />
    public IReadOnlyList<Permission> Permissions { get; internal set; } = ArraySegment<Permission>.Empty;

    /// <inheritdoc />
    public PluginInfo PluginInfo { get; internal set; } = null!;

    /// <inheritdoc />
    public IPluginManager PluginManager { get; internal set; } = null!;

    /// <inheritdoc />
    public IServiceProvider ServiceProvider { get; internal set; } = null!;

    /// <inheritdoc />
    public virtual void Dispose()
    {
    }

    /// <inheritdoc />
    public Permission? GetPermission(string name)
    {
        for (var index = 0; index < Permissions.Count; index++)
        {
            if (string.Equals(name, Permissions[index].Name, StringComparison.Ordinal))
                return Permissions[index];
        }

        return null;
    }

    /// <inheritdoc />
    ~MonoPlugin()
    {
        Dispose();
    }

    /// <summary>
    ///     Allows configuration of the plugin's <see cref="IServiceProvider" />.
    /// </summary>
    /// <param name="services">The service collection.</param>
    protected internal virtual void ConfigureServices(IServiceCollection services)
    {
    }

    /// <summary>
    ///     Called when this plugin is disabled.
    /// </summary>
    protected internal virtual Task OnDisable()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Called when this plugin is enabled.
    /// </summary>
    protected internal virtual Task OnEnable()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Called when this plugin is loaded.
    /// </summary>
    /// <remarks>This method is always called upon a plugin's load, even if the plugin is not yet enabled.</remarks>
    /// <remarks>Event subscriptions for <see cref="DiscordClient" /> should be placed here.</remarks>
    protected internal virtual Task OnLoad()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Called when this plugin is unloaded.
    /// </summary>
    /// <remarks>This method is always called upon a plugin's unload, even if the plugin is not currently enabled.</remarks>
    protected internal virtual Task OnUnload()
    {
        return Task.CompletedTask;
    }
}
