using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;

namespace BrackeysBot.API.Permissions;

/// <summary>
///     Represents a permission.
/// </summary>
public sealed class Permission : IEquatable<Permission>
{
    [JsonPropertyName("ids")] [JsonInclude]
    private string[] _ids;

    [JsonConstructor]
    private Permission()
    {
        _ids = Array.Empty<string>();
        Name = string.Empty;
        Type = PermissionType.Everyone;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Permission" /> class.
    /// </summary>
    /// <param name="name">The name of the permission.</param>
    /// <param name="type">The type of the permission.</param>
    /// <param name="ids">The set of applicable IDs.</param>
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="name" /> is <see langword="null" />, empty, or consists of only whitespace.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     <paramref name="type" /> is not a value defined in <see cref="PermissionType" />.
    /// </exception>
    public Permission(string name, PermissionType type, params string[] ids)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (!Enum.IsDefined(type)) throw new ArgumentOutOfRangeException(nameof(type));

        Name = name;
        Type = type;
        _ids = ids is {Length: > 0} ? ids.ToArray() /* defensive copy */ : Array.Empty<string>();
    }

    /// <summary>
    ///     Gets the set of IDs applicable to this permission.
    /// </summary>
    /// <value>The set of applicable IDs.</value>
    public IReadOnlyList<string> Ids => _ids.ToArray(); // defensive copy

    /// <summary>
    ///     Gets the name of the permission.
    /// </summary>
    /// <value>The name of the permission.</value>
    [JsonPropertyName("name")]
    [JsonInclude]
    public string Name { get; private set; }

    /// <summary>
    ///     Gets the type of the permission.
    /// </summary>
    /// <value>The type of the permission.</value>
    [JsonPropertyName("type")]
    [JsonInclude]
    public PermissionType Type { get; private set; }

    /// <inheritdoc />
    public bool Equals(Permission? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name;
    }

    public static bool operator ==(Permission? left, Permission? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Permission? left, Permission? right)
    {
        return !(left == right);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is Permission other && Equals(other));
    }

    /// <inheritdoc />
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
