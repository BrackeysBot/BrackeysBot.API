using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.API.Abstractions.Rest;
using Remora.Discord.Rest.API;

namespace BrackeysBot.API.Extensions;

/// <summary>
///     Extension methods for <see cref="IChannel" />.
/// </summary>
public static class DiscordChannelExtensions
{
    /// <summary>
    ///     Gets the category of this channel.
    /// </summary>
    /// <param name="channel">The channel whose category to retrieve.</param>
    /// <param name="serviceProvider">
    ///     A service provider object containing a <see cref="IDiscordRestChannelAPI" /> from which the category will be
    ///     retrieved.
    /// </param>
    /// <returns>The category of this channel, or <see langword="null" /> if this channel is not defined in a category.</returns>
    /// <exception cref="ArgumentNullException">
    ///     <para><paramref name="channel" /> is <see langword="null" />.</para>
    ///     -or-
    ///     <para><paramref name="serviceProvider" /> is <see langword="null" />.</para>
    /// </exception>
    public static async Task<IChannel?> GetCategoryAsync(this IChannel channel, IServiceProvider serviceProvider)
    {
        if (channel is null) throw new ArgumentNullException(nameof(channel));
        if (serviceProvider is null) throw new ArgumentNullException(nameof(serviceProvider));

        var channelApi = serviceProvider.GetRequiredService<IDiscordRestChannelAPI>();

        while (true)
        {
            if (channel.Type == ChannelType.GuildCategory) return channel;
            if (!channel.ParentID.HasValue || channel.ParentID.Value is not { } snowflake) return null;
            channel = (await channelApi.GetChannelAsync(snowflake)).Entity;
        }
    }
}
