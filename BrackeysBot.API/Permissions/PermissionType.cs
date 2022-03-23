namespace BrackeysBot.API.Permissions;

/// <summary>
///     An enumeration of permission types.
/// </summary>
public enum PermissionType
{
    /// <summary>
    ///     Defines that the permission is granted to everybody.
    /// </summary>
    Everyone,

    /// <summary>
    ///     Defines that the permission is granted to a set of roles.
    /// </summary>
    Role,

    /// <summary>
    ///     Defines that the permission is granted to a set of users.
    /// </summary>
    User
}
