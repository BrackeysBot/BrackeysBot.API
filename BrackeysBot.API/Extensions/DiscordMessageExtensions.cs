using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.API.Abstractions.Rest;
using Remora.Rest.Core;

namespace BrackeysBot.API.Extensions;

/// <summary>
///     Extension methods for <see cref="IMessage" />.
/// </summary>
public static class DiscordMessageExtensions
{
    /// <summary>
    ///     Acknowledges a message by reacting to it.
    /// </summary>
    /// <param name="message">The message to acknowledge.</param>
    /// <param name="serviceProvider">
    ///     The service provider whose <see cref="IDiscordRestChannelAPI" /> will be used to react to the message.
    /// </param>
    public static Task AcknowledgeAsync(this IMessage message, IServiceProvider serviceProvider)
    {
        return message.AcknowledgeAsync(serviceProvider.GetRequiredService<IDiscordRestChannelAPI>());
    }

    /// <summary>
    ///     Acknowledges a message by reacting to it.
    /// </summary>
    /// <param name="message">The message to acknowledge.</param>
    /// <param name="channelApi">
    ///     The <see cref="IDiscordRestChannelAPI" /> to be used to react to the message.
    /// </param>
    public static Task AcknowledgeAsync(this IMessage message, IDiscordRestChannelAPI channelApi)
    {
        return channelApi.CreateReactionAsync(message.ChannelID, message.ID, "✅");
    }

    /// <summary>
    ///     Deletes this message after a specified delay.
    /// </summary>
    /// <param name="message">The message to delete.</param>
    /// <param name="serviceProvider">
    ///     The service provider whose <see cref="IDiscordRestChannelAPI" /> will be used to delete the message.
    /// </param>
    /// <param name="delay">The delay before deletion.</param>
    /// <param name="reason">The reason for the deletion.</param>
    public static Task DeleteAfterAsync(this IMessage message, IServiceProvider serviceProvider, TimeSpan delay,
        string? reason = null)
    {
        return message.DeleteAfterAsync(serviceProvider.GetRequiredService<IDiscordRestChannelAPI>(), delay, reason);
    }

    /// <summary>
    ///     Deletes this message after a specified delay.
    /// </summary>
    /// <param name="message">The message to delete.</param>
    /// <param name="channelApi">
    ///     The <see cref="IDiscordRestChannelAPI" /> to be used to delete the message.
    /// </param>
    /// <param name="delay">The delay before deletion.</param>
    /// <param name="reason">The reason for the deletion.</param>
    public static Task DeleteAfterAsync(this IMessage message, IDiscordRestChannelAPI channelApi, TimeSpan delay,
        string? reason = null)
    {
        Optional<string> optionalReason = reason ?? new Optional<string>();
        return Task.Delay(delay).ContinueWith(_ => channelApi.DeleteMessageAsync(message.ChannelID, message.ID, optionalReason));
    }
}
