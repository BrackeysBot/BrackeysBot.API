using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace BrackeysBot.API.Extensions;

/// <summary>
///     Extension methods for <see cref="DiscordMessage" />.
/// </summary>
public static class DiscordMessageExtensions
{
    /// <summary>
    ///     Acknowledges a message by reacting to it.
    /// </summary>
    /// <param name="message">The message to acknowledge.</param>
    public static Task AcknowledgeAsync(this DiscordMessage message)
    {
        return message.CreateReactionAsync(DiscordEmoji.FromUnicode("✅"));
    }

    /// <summary>
    ///     Deletes this message after a specified delay.
    /// </summary>
    /// <param name="message">The message to delete.</param>
    /// <param name="delay">The delay before deletion.</param>
    /// <param name="reason">The reason for the deletion.</param>
    public static Task DeleteAfterAsync(this DiscordMessage message, TimeSpan delay, string? reason = null)
    {
        return Task.Delay(delay).ContinueWith(_ => message.DeleteAsync(reason));
    }

    /// <summary>
    ///     Deletes the message as created by this task after a specified delay.
    /// </summary>
    /// <param name="task">The task whose <see cref="DiscordMessage" /> result should be deleted.</param>
    /// <param name="delay">The delay before deletion.</param>
    /// <param name="reason">The reason for the deletion.</param>
    public static Task DeleteAfterAsync(this Task<DiscordMessage> task, TimeSpan delay, string? reason = null)
    {
        return task.ContinueWith(message => message.DeleteAfterAsync(delay, reason));
    }

    /// <summary>
    ///     Normalizes a <see cref="DiscordMessage" /> so that the internal client is assured to be a specified value.
    /// </summary>
    /// <param name="message">The <see cref="DiscordMessage" /> to normalize.</param>
    /// <param name="client">The target client.</param>
    /// <returns>
    ///     A <see cref="DiscordMessage" /> whose public values will match <paramref name="message" />, but whose internal client
    ///     is <paramref name="client" />.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     <para><paramref name="message" /> is <see langword="null" /></para>
    ///     -or-
    ///     <para><paramref name="client" /> is <see langword="null" /></para>
    /// </exception>
    public static async Task<DiscordMessage> NormalizeClientAsync(this DiscordMessage message, DiscordClient client)
    {
        if (message is null) throw new ArgumentNullException(nameof(message));
        if (client is null) throw new ArgumentNullException(nameof(client));

        DiscordChannel channel = await message.Channel.NormalizeClientAsync(client);
        return await channel.GetMessageAsync(message.Id);
    }
}
