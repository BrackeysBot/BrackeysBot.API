using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;

namespace BrackeysBot.API.Extensions;

/// <summary>
///     Extension methods for <see cref="DiscordUser" />.
/// </summary>
public static class DiscordUserExtensions
{
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
