using System;
using System.Threading;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;

namespace BrackeysBot.API.Interactivity;

/// <summary>
///     Provides shared state for a <see cref="Conversation" />.
/// </summary>
/// <remarks>This API is experimental, and is subject to sudden undocumented changes!</remarks>
public sealed class ConversationContext
{
    private ConversationContext(IServiceProvider serviceProvider, DiscordUser user, DiscordChannel channel)
    {
        Services = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        Client = serviceProvider.GetRequiredService<DiscordClient>();
        Channel = channel ?? throw new ArgumentNullException(nameof(channel));
        User = user ?? throw new ArgumentNullException(nameof(user));
        Member = user as DiscordMember;
    }

    /// <summary>
    ///     Gets the cancellation token source for this conversation.
    /// </summary>
    /// <value>The cancellation token source.</value>
    public CancellationTokenSource CancellationTokenSource { get; } = new();

    /// <summary>
    ///     Gets the underlying <see cref="DiscordClient" /> owning the conversation.
    /// </summary>
    /// <value>The <see cref="DiscordClient" />.</value>
    public DiscordClient Client { get; }

    /// <summary>
    ///     Gets the channel in which the conversation is being held.
    /// </summary>
    /// <value>The channel.</value>
    public DiscordChannel Channel { get; }

    /// <summary>
    ///     Gets the guild in which the conversation is being held, if any.
    /// </summary>
    public DiscordGuild? Guild => Channel.Guild;

    /// <summary>
    ///     Gets the member who initiated the conversation, if this conversation is in a guild.
    /// </summary>
    /// <value>The member.</value>
    public DiscordMember? Member { get; set; }

    /// <summary>
    ///     Gets the service provider for the conversation.
    /// </summary>
    /// <value>The service provider.</value>
    public IServiceProvider Services { get; }

    /// <summary>
    ///     Gets the user who initiated the conversation.
    /// </summary>
    /// <value>The user.</value>
    public DiscordUser User { get; }

    /// <summary>
    ///     Constructs a new <see cref="ConversationContext" /> from a specified <see cref="CommandContext" />.
    /// </summary>
    /// <param name="context">The <see cref="CommandContext" /> from which the values should be pulled.</param>
    /// <returns>A new instance of <see cref="ConversationContext" />.</returns>
    public static ConversationContext FromCommandContext(CommandContext context)
    {
        return new ConversationContext(context.Services, context.Member ?? context.User, context.Channel);
    }

    /// <summary>
    ///     Constructs a new <see cref="ConversationContext" /> from a specified <see cref="ContextMenuContext" />.
    /// </summary>
    /// <param name="context">The <see cref="ContextMenuContext" /> from which the values should be pulled.</param>
    /// <returns>A new instance of <see cref="ConversationContext" />.</returns>
    public static ConversationContext FromContextMenuContext(ContextMenuContext context)
    {
        return new ConversationContext(context.Services, context.Member ?? context.User, context.Channel);
    }

    /// <summary>
    ///     Constructs a new <see cref="ConversationContext" /> from a specified <see cref="InteractionContext" />.
    /// </summary>
    /// <param name="context">The <see cref="InteractionContext" /> from which the values should be pulled.</param>
    /// <returns>A new instance of <see cref="ConversationContext" />.</returns>
    public static ConversationContext FromInteractionContext(InteractionContext context)
    {
        return new ConversationContext(context.Services, context.Member ?? context.User, context.Channel);
    }
}
