using System;
using System.Threading.Tasks;
using BrackeysBot.API.Extensions;
using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;

namespace BrackeysBot.API.Permissions;

/// <summary>
///     Defines valid permissions for this command or group.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class RequirePermissionAttribute : CheckBaseAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RequirePermissionAttribute" /> class.
    /// </summary>
    /// <param name="permissionName">The permission node name.</param>
    public RequirePermissionAttribute(string permissionName)
    {
        if (string.IsNullOrWhiteSpace(permissionName)) throw new ArgumentNullException(nameof(permissionName));
        PermissionName = permissionName;
    }

    /// <summary>
    ///     Gets or sets the name of the permission node.
    /// </summary>
    /// <value>The permission node name.</value>
    public string PermissionName { get; }

    /// <inheritdoc />
    public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
    {
        return Task.FromResult(ctx.HasPermission(PermissionName));
    }
}
