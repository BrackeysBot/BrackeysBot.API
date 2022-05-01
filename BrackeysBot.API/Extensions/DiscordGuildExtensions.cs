using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.API.Abstractions.Rest;

namespace BrackeysBot.API.Extensions;

/// <summary>
///     Extension methods for <see cref="IGuild" />.
/// </summary>
public static class DiscordGuildExtensions
{
    /// <summary>
    ///     Joins all active threads in the guild that this client has permission to view.
    /// </summary>
    /// <param name="guild">The guild whose active threads to join.</param>
    /// <param name="serviceProvider">
    ///     The service provider whose <see cref="IDiscordRestChannelAPI" /> will be used to join threads.
    /// </param>
    public static Task JoinAllThreadsAsync(this IGuild guild, IServiceProvider serviceProvider)
    {
        return guild.JoinAllThreadsAsync(serviceProvider.GetRequiredService<IDiscordRestChannelAPI>());
    }

    /// <summary>
    ///     Joins all active threads in the guild that this client has permission to view.
    /// </summary>
    /// <param name="guild">The guild whose active threads to join.</param>
    /// <param name="channelApi">
    ///     The <see cref="IDiscordRestChannelAPI" /> to be used to join threads.
    /// </param>
    public static Task JoinAllThreadsAsync(this IGuild guild, IDiscordRestChannelAPI channelApi)
    {
        return Task.WhenAll(guild.Threads.Value.Select(t => channelApi.JoinThreadAsync(t.ID)));
    }
}
