using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;

namespace BrackeysBot.API.Extensions;

/// <summary>
///     Extension methods for <see cref="DiscordUser" />.
/// </summary>
public static class DiscordUserExtensions
{
    /// <summary>
    ///     Returns the current user as a member of a specified guild.
    /// </summary>
    /// <param name="user">The user to transform.</param>
    /// <param name="guild">The guild whose member list to search.</param>
    /// <returns>
    ///     The correlated <see cref="DiscordMember" />, or <see langword="null" /> if this user is not in
    ///     <paramref name="guild" />.
    /// </returns>
    public static async Task<DiscordMember?> GetAsMemberAsync(this DiscordUser user, DiscordGuild guild)
    {
        if (guild.Members.TryGetValue(user.Id, out DiscordMember? member))
            return member;

        try
        {
            return await guild.GetMemberAsync(user.Id);
        }
        catch (NotFoundException)
        {
            return null;
        }
    }

    /// <summary>
    ///     Returns the user's username with the discriminator, in the format <c>username#discriminator</c>.
    /// </summary>
    /// <param name="user">The user whose username and discriminator to retrieve.</param>
    /// <returns>A string in the format <c>username#discriminator</c></returns>
    /// <exception cref="ArgumentNullException"><paramref name="user" /> is <see langword="null" />.</exception>
    public static string GetUsernameWithDiscriminator(this DiscordUser user)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        return $"{user.Username}#{user.Discriminator}";
    }

    /// <summary>
    ///     Returns a value indicating whether the user is a member of the specified guild.
    /// </summary>
    /// <param name="user">The user whose member status to check.</param>
    /// <param name="guild">The guild whose member list to search.</param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="user" /> is a member of <paramref name="guild" />; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public static async Task<bool> IsMemberOfAsync(this DiscordUser user, DiscordGuild guild)
    {
        if (guild.Members.TryGetValue(user.Id, out DiscordMember? _))
            return true;

        try
        {
            await guild.GetMemberAsync(user.Id);
            return true;
        }
        catch (NotFoundException)
        {
            return false;
        }
    }

    /// <summary>
    ///     Normalizes a <see cref="DiscordUser" /> so that the internal client is assured to be a specified value.
    /// </summary>
    /// <param name="user">The <see cref="DiscordUser" /> to normalize.</param>
    /// <param name="client">The target client.</param>
    /// <returns>
    ///     A <see cref="DiscordUser" /> whose public values will match <paramref name="user" />, but whose internal client
    ///     is <paramref name="client" />.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     <para><paramref name="user" /> is <see langword="null" /></para>
    ///     -or-
    ///     <para><paramref name="client" /> is <see langword="null" /></para>
    /// </exception>
    public static Task<DiscordUser> NormalizeClientAsync(this DiscordUser user, DiscordClient client)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        if (client is null) throw new ArgumentNullException(nameof(client));

        return client.GetUserAsync(user.Id);
    }
}
