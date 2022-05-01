using System;
using System.Drawing;
using System.Linq;
using Remora.Discord.API.Abstractions.Objects;

namespace BrackeysBot.API.Extensions;

/// <summary>
///     Extension methods for <see cref="IGuildMember" />.
/// </summary>
public static class GuildMemberExtensions
{
    /// <summary>
    ///     Retrieves the color of a member in a specified guild.
    /// </summary>
    /// <param name="member">The member whose colour to retrieve.</param>
    /// <param name="guild">The guild whose roles to search.</param>
    /// <returns>
    ///     The color of the member's highest role. If the user has no roles, <see cref="Color.Transparent" /> is returned.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    ///     <para><paramref name="member" /> is <see langword="null" />.</para>
    ///     -or-
    ///     <para><paramref name="guild" /> is <see langword="null" />.</para>
    /// </exception>
    public static Color GetColor(this IGuildMember member, IGuild guild)
    {
        if (member is null) throw new ArgumentNullException(nameof(member));
        if (guild is null) throw new ArgumentNullException(nameof(guild));
        return guild.Roles.OrderByDescending(r => r.Position).FirstOrDefault(r => member.Roles.Contains(r.ID))?.Colour
               ?? Color.Transparent;
    }
}
