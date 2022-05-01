using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Remora.Rest.Core;

namespace BrackeysBot.API.Data.ValueConverters;

/// <summary>
///     Converts a <see cref="Snowflake" /> to and from an unsigned 64-bit integer.
/// </summary>
public sealed class SnowflakeToUInt64Converter : ValueConverter<Snowflake, ulong>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SnowflakeToUInt64Converter" /> class.
    /// </summary>
    public SnowflakeToUInt64Converter()
        : this(null)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="SnowflakeToUInt64Converter" /> class.
    /// </summary>
    public SnowflakeToUInt64Converter(ConverterMappingHints? mappingHints)
        : base(v => v.Value, v => new Snowflake(v, 0), mappingHints)
    {
    }
}
