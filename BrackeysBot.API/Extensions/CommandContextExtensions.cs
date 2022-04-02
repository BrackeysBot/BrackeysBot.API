using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrackeysBot.API.Permissions;
using BrackeysBot.API.Plugins;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace BrackeysBot.API.Extensions;

/// <summary>
///     Extension methods for <see cref="CommandContext" />.
/// </summary>
public static class CommandContextExtensions
{
    /// <summary>
    ///     Acknowledges the message provided by a <see cref="CommandContext" /> by reacting to it.
    /// </summary>
    /// <param name="context">The command context.</param>
    public static Task AcknowledgeAsync(this CommandContext context)
    {
        return context.Message.AcknowledgeAsync();
    }

    /// <summary>
    ///     Returns a value indicating whether the user which invoked the command has a specified permission.
    /// </summary>
    /// <param name="context">The command context.</param>
    /// <param name="permission">The permission to validate.</param>
    /// <returns>
    ///     <see langword="true" /> if the invoking user has the specified permission; otherwise, <see langword="false" />.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="permission" /> is <see langword="null" />.</exception>
    public static bool HasPermission(this CommandContext context, Permission permission)
    {
        if (permission is null) throw new ArgumentNullException(nameof(permission));

        PermissionType permissionType = permission.Type;

        if (context.Member is not { } member)
        {
            if (context.Command.ExecutionChecks.Any(check => check is RequireGuildAttribute))
                return false;

            return permissionType == PermissionType.Everyone ||
                   (permissionType == PermissionType.User && FilterPermissionIds(permission.Ids).Contains(context.User.Id));
        }

        switch (permissionType)
        {
            case PermissionType.Everyone:
                return true;

            case PermissionType.Role:
                IEnumerable<ulong> roleIds = member.Roles.Select(r => r.Id);
                return FilterPermissionIds(permission.Ids).Any(id => roleIds.Contains(id));

            case PermissionType.User:
                return FilterPermissionIds(permission.Ids).Contains(member.Id);

            default:
                return false;
        }
    }

    /// <summary>
    ///     Returns a value indicating whether the user which invoked the command has a specified permission.
    /// </summary>
    /// <param name="context">The command context.</param>
    /// <param name="permissionName">The permission to validate.</param>
    /// <returns>
    ///     <see langword="true" /> if the invoking user has the specified permission; otherwise, <see langword="false" />.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="permissionName" /> is <see langword="null" />.</exception>
    public static bool HasPermission(this CommandContext context, string permissionName)
    {
        var plugin = context.Services.GetRequiredService<IPlugin>();
        Permission? permission = plugin.GetPermission(permissionName);
        return permission is not null && context.HasPermission(permission);
    }

    private static IEnumerable<ulong> FilterPermissionIds(IEnumerable<string> source)
    {
        return source.Select(value =>
        {
            if (value.StartsWith('-') && ulong.TryParse(value[1..], out ulong id))
                return id;
            return ulong.TryParse(value, out id) ? id : 0;
        }).Where(id => id > 0);
    }
}
