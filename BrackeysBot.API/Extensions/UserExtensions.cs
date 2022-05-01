using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.API.Abstractions.Rest;
using Remora.Results;

namespace BrackeysBot.API.Extensions;

/// <summary>
///     Extension methods for <see cref="IUser" />.
/// </summary>
public static class UserExtensions
{
    /// <summary>
    ///     Returns the current user as a member of a specified guild.
    /// </summary>
    /// <param name="user">The user to transform.</param>
    /// <param name="guild">The guild whose member list to search.</param>
    /// <param name="serviceProvider">
    ///     The service provider whose <see cref="IDiscordRestGuildAPI" /> will be used to retrieve the member.
    /// </param>
    /// <returns>
    ///     The correlated <see cref="IGuildMember" />, or <see langword="null" /> if this user is not in
    ///     <paramref name="guild" />.
    /// </returns>
    public static Task<IGuildMember?> GetAsMemberAsync(this IUser user, IGuild guild, IServiceProvider serviceProvider)
    {
        var guildApi = serviceProvider.GetRequiredService<IDiscordRestGuildAPI>();
        return user.GetAsMemberAsync(guild, guildApi);
    }

    /// <summary>
    ///     Returns the current user as a member of a specified guild.
    /// </summary>
    /// <param name="user">The user to transform.</param>
    /// <param name="guild">The guild whose member list to search.</param>
    /// <param name="guildApi">
    ///     The <see cref="IDiscordRestGuildAPI" /> to be used to retrieve the member.
    /// </param>
    /// <returns>
    ///     The correlated <see cref="IGuildMember" />, or <see langword="null" /> if this user is not in
    ///     <paramref name="guild" />.
    /// </returns>
    public static async Task<IGuildMember?> GetAsMemberAsync(this IUser user, IGuild guild, IDiscordRestGuildAPI guildApi)
    {
        Result<IGuildMember> result = await guildApi.GetGuildMemberAsync(guild.ID, user.ID);
        return result.IsSuccess ? result.Entity : null;
    }

    /// <summary>
    ///     Returns the user's username with the discriminator, in the format <c>username#discriminator</c>.
    /// </summary>
    /// <param name="user">The user whose username and discriminator to retrieve.</param>
    /// <returns>A string in the format <c>username#discriminator</c></returns>
    /// <exception cref="ArgumentNullException"><paramref name="user" /> is <see langword="null" />.</exception>
    public static string GetUsernameWithDiscriminator(this IUser user)
    {
        if (user is null) throw new ArgumentNullException(nameof(user));
        return $"{user.Username}#{user.Discriminator:0000}";
    }

    /// <summary>
    ///     Returns a value indicating whether the user is a member of the specified guild.
    /// </summary>
    /// <param name="user">The user whose member status to check.</param>
    /// <param name="guild">The guild whose member list to search.</param>
    /// <param name="serviceProvider">
    ///     The service provider whose <see cref="IDiscordRestGuildAPI" /> will be used to retrieve the member.
    /// </param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="user" /> is a member of <paramref name="guild" />; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public static Task<bool> IsMemberOfAsync(this IUser user, IGuild guild, IServiceProvider serviceProvider)
    {
        return user.IsMemberOfAsync(guild, serviceProvider.GetRequiredService<IDiscordRestGuildAPI>());
    }

    /// <summary>
    ///     Returns a value indicating whether the user is a member of the specified guild.
    /// </summary>
    /// <param name="user">The user whose member status to check.</param>
    /// <param name="guild">The guild whose member list to search.</param>
    /// <param name="guildApi">
    ///     The <see cref="IDiscordRestGuildAPI" /> to be used to retrieve the member.
    /// </param>
    /// <returns>
    ///     <see langword="true" /> if <paramref name="user" /> is a member of <paramref name="guild" />; otherwise,
    ///     <see langword="false" />.
    /// </returns>
    public static async Task<bool> IsMemberOfAsync(this IUser user, IGuild guild, IDiscordRestGuildAPI guildApi)
    {
        Result<IGuildMember> result = await guildApi.GetGuildMemberAsync(guild.ID, user.ID);
        return result.IsSuccess;
    }
}
