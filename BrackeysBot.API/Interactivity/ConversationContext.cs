using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.API.Abstractions.Rest;
using Remora.Discord.API.Objects;
using Remora.Discord.Commands.Contexts;
using Remora.Results;

namespace BrackeysBot.API.Interactivity;

/// <summary>
///     Provides shared state for a <see cref="Conversation" />.
/// </summary>
/// <remarks>This API is experimental, and is subject to sudden undocumented changes!</remarks>
public sealed class ConversationContext
{
    private ConversationContext(IServiceProvider serviceProvider, IChannel channel, IUser user, IGuildMember? member)
    {
        Services = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        Channel = channel ?? throw new ArgumentNullException(nameof(channel));
        User = user ?? throw new ArgumentNullException(nameof(user));
        Member = member;
    }

    /// <summary>
    ///     Gets the cancellation token source for this conversation.
    /// </summary>
    /// <value>The cancellation token source.</value>
    public CancellationTokenSource CancellationTokenSource { get; } = new();

    /// <summary>
    ///     Gets the channel in which the conversation is being held.
    /// </summary>
    /// <value>The channel.</value>
    public IChannel Channel { get; }

    /// <summary>
    ///     Gets the interaction which triggered this conversation, if any.
    /// </summary>
    /// <value>
    ///     The interaction, or <see langword="null" /> if this conversation was not initiated by an application command.
    /// </value>
    public InteractionContext? InteractionContext { get; private init; }

    /// <summary>
    ///     Gets the member who initiated the conversation, if this conversation is in a guild.
    /// </summary>
    /// <value>The member.</value>
    public IGuildMember? Member { get; }

    /// <summary>
    ///     Gets the message, sent by the user, that initiated the conversation, if any.
    /// </summary>
    /// <value>The original message, or <see langword="null" /> if no message is associated with this conversation.</value>
    public IMessage? Message { get; private init; }

    /// <summary>
    ///     Gets the service provider for the conversation.
    /// </summary>
    /// <value>The service provider.</value>
    public IServiceProvider Services { get; }

    /// <summary>
    ///     Gets the user who initiated the conversation.
    /// </summary>
    /// <value>The user.</value>
    public IUser User { get; }

    /// <summary>
    ///     Constructs a new <see cref="ConversationContext" /> from a specified <see cref="InteractionContext" />.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="context">The <see cref="InteractionContext" /> from which the values should be pulled.</param>
    /// <returns>A new instance of <see cref="ConversationContext" />.</returns>
    public static async Task<ConversationContext> FromInteractionContextContextAsync(IServiceProvider serviceProvider,
        InteractionContext context)
    {
        var channelApi = serviceProvider.GetRequiredService<IDiscordRestChannelAPI>();
        Result<IChannel> channel = await channelApi.GetChannelAsync(context.ChannelID);
        IGuildMember? member = context.Member.HasValue ? context.Member.Value : null;
        IMessage? message = context.Message.HasValue ? context.Message.Value : null;

        return new ConversationContext(serviceProvider, channel.Entity, context.User, member)
        {
            InteractionContext = context,
            Message = message
        };
    }


    /// <summary>
    ///     Responds to the original message with the specified content. If <see cref="Message" /> is <see langword="null" />, a
    ///     new message is sent in <see cref="Channel" />; otherwise, a message as sent with a reply to <see cref="Message" />.
    /// </summary>
    /// <param name="content">The message content.</param>
    /// <returns>The message response.</returns>
    public async Task<IMessage> RespondAsync(string content)
    {
        var channelApi = Services.GetRequiredService<IDiscordRestChannelAPI>();
        Result<IMessage> result = await channelApi.CreateMessageAsync(Channel.ID, content);
        return result.Entity;
    }


    /// <summary>
    ///     Responds to the original message with the specified content and embed. If <see cref="Message" /> is
    ///     <see langword="null" />, a new message is sent in <see cref="Channel" />; otherwise, a message as sent with a reply to
    ///     <see cref="Message" />.
    /// </summary>
    /// <param name="content">The message content.</param>
    /// <param name="embed">The embed to attach.</param>
    /// <returns>The message response.</returns>
    public async Task<IMessage> RespondAsync(string content, Embed embed)
    {
        var channelApi = Services.GetRequiredService<IDiscordRestChannelAPI>();
        Result<IMessage> result = await channelApi.CreateMessageAsync(Channel.ID, content, embeds: new[] {embed});
        return result.Entity;
    }

    /// <summary>
    ///     Responds to the original message with the specified embed. If <see cref="Message" /> is <see langword="null" />, a
    ///     new message is sent in <see cref="Channel" />; otherwise, a message as sent with a reply to <see cref="Message" />.
    /// </summary>
    /// <param name="embed">The embed to attach.</param>
    /// <returns>The message response.</returns>
    public async Task<IMessage> RespondAsync(Embed embed)
    {
        var channelApi = Services.GetRequiredService<IDiscordRestChannelAPI>();
        Result<IMessage> result = await channelApi.CreateMessageAsync(Channel.ID, embeds: new[] {embed});
        return result.Entity;
    }
}
