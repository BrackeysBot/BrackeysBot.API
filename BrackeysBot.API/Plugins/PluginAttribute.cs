using System;

namespace BrackeysBot.API.Plugins;

/// <summary>
///     Specifies <see cref="MonoPlugin" /> information such as the plugin's name and version.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class PluginAttribute : Attribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PluginAttribute" /> class.
    /// </summary>
    /// <param name="name">The name of this plugin.</param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="name" /> is <see langword="null" />, empty, or consists of only whitespace.
    /// </exception>
    public PluginAttribute(string name)
    {
        Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(name) : name;
    }

    /// <summary>
    ///     Gets the name of the plugin.
    /// </summary>
    /// <value>The name of the plugin.</value>
    public string Name { get; }
}
