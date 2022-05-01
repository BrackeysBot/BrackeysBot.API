using System;
using System.Diagnostics.CodeAnalysis;

namespace BrackeysBot.API;

/// <summary>
///     Provides helper methods for Discord formatting.
/// </summary>
public static class FormatUtility
{
    /// <summary>
    ///     Returns a string with the specified text surrounded by the markdown bold specifier.
    /// </summary>
    /// <param name="value">The value to encapsulate as bold.</param>
    /// <returns>
    ///     <paramref name="value" /> surrounded by <c>**</c>, or <see langword="null" /> if <paramref name="value" /> is
    ///     <see langword="null" />.
    /// </returns>
    [return: NotNullIfNotNull("value")]
    public static string? Bold(string? value)
    {
        return value is null ? null : $"**{value}**";
    }

    /// <summary>
    ///     Returns a string with the specified text surrounded by the markdown code block specifier.
    /// </summary>
    /// <param name="value">The value to encapsulate as a code block.</param>
    /// <param name="language">The language to use for syntax highlighting.</param>
    /// <returns>
    ///     <paramref name="value" /> surrounded by <c>`</c>, or <see langword="null" /> if <paramref name="value" /> is
    ///     <see langword="null" />.
    /// </returns>
    [return: NotNullIfNotNull("value")]
    public static string? CodeBlock(string? value, string? language = null)
    {
        return value is null ? null : $"```{language}\n{value}\n```";
    }

    /// <summary>
    ///     Returns a string with the specified text surrounded by the markdown italic specifier.
    /// </summary>
    /// <param name="value">The value to encapsulate as italic.</param>
    /// <returns>
    ///     <paramref name="value" /> surrounded by <c>*</c>, or <see langword="null" /> if <paramref name="value" /> is
    ///     <see langword="null" />.
    /// </returns>
    [return: NotNullIfNotNull("value")]
    public static string? Italic(string? value)
    {
        return value is null ? null : $"*{value}*";
    }

    /// <summary>
    ///     Returns a string with the specified text surrounded by the markdown inline code specifier.
    /// </summary>
    /// <param name="value">The value to encapsulate as inline code.</param>
    /// <returns>
    ///     <paramref name="value" /> surrounded by <c>`</c>, or <see langword="null" /> if <paramref name="value" /> is
    ///     <see langword="null" />.
    /// </returns>
    [return: NotNullIfNotNull("value")]
    public static string? InlineCode(string? value)
    {
        return value is null ? null : $"`{value}`";
    }

    /// <summary>
    ///     Returns a string with the specified text surrounded by the markdown inline code specifier.
    /// </summary>
    /// <param name="value">The value to encapsulate as inline code.</param>
    /// <returns>
    ///     <paramref name="value" /> surrounded by <c>`</c>, or <see langword="null" /> if <paramref name="value" /> is
    ///     <see langword="null" />.
    /// </returns>
    [return: NotNullIfNotNull("value")]
    public static string? Sanitize(string? value)
    {
        if (value is null) return null;
        Span<char> buffer = stackalloc char[value.Length * 2];

        var index = 0;
        foreach (char character in value)
        {
            if (character is '`' or '\\' or '*' or '_' or '~' or '<' or '>' or '[' or ']' or '(' or ')' or '"' or '@' or '!'
                or '&' or '#' or ':' or '|')
            {
                buffer[index++] = '\\';
            }

            buffer[index++] = character;
        }

        return new string(buffer[..index]);
    }

    /// <summary>
    ///     Returns a string with the specified text surrounded by the markdown spoiler specifier.
    /// </summary>
    /// <param name="value">The value to encapsulate as spoiler.</param>
    /// <returns>
    ///     <paramref name="value" /> surrounded by <c>||</c>, or <see langword="null" /> if <paramref name="value" /> is
    ///     <see langword="null" />.
    /// </returns>
    [return: NotNullIfNotNull("value")]
    public static string? Spoiler(string? value)
    {
        return value is null ? null : $"||{value}||";
    }

    /// <summary>
    ///     Returns a string with the specified text surrounded by the markdown strikeout specifier.
    /// </summary>
    /// <param name="value">The value to encapsulate as strikeout.</param>
    /// <returns>
    ///     <paramref name="value" /> surrounded by <c>~~</c>, or <see langword="null" /> if <paramref name="value" /> is
    ///     <see langword="null" />.
    /// </returns>
    [return: NotNullIfNotNull("value")]
    public static string? Strikeout(string? value)
    {
        return value is null ? null : $"~~{value}~~";
    }

    /// <summary>
    ///     Returns a string with the specified text surrounded by the markdown inline code specifier.
    /// </summary>
    /// <param name="value">The value to encapsulate as inline code.</param>
    /// <returns>
    ///     <paramref name="value" /> surrounded by <c>`</c>, or <see langword="null" /> if <paramref name="value" /> is
    ///     <see langword="null" />.
    /// </returns>
    [return: NotNullIfNotNull("value")]
    public static string? Strip(string? value)
    {
        if (value is null) return null;
        Span<char> buffer = stackalloc char[value.Length];

        var index = 0;

        for (var stringIndex = 0; stringIndex < value.Length; stringIndex++)
        {
            char character = value[stringIndex];

            if (character is '`' or '*' or '_' or '~' or '[' or ']' or '(' or ')' or '"' or '|')
            {
                continue;
            }

            if (character is '<')
            {
                int endIndex = value.IndexOf('>', stringIndex);
                string temp = value[(stringIndex + 1)..endIndex];

                if (temp[0] is '@' or '#')
                {
                    string id = temp[1..];
                    if (temp[1] is '!' or '&') id = id[1..];

                    if (ulong.TryParse(id, out _))
                    {
                        stringIndex += temp.Length + 1;
                        continue;
                    }
                }
                else if (temp[0] is ':')
                {
                    endIndex = temp.IndexOf(':', 1);
                    string id = temp[(endIndex + 1)..];

                    if (ulong.TryParse(id, out _))
                    {
                        stringIndex += temp.Length + 1;
                        continue;
                    }
                }
            }

            buffer[index++] = character;
        }

        return new string(buffer[..index]);
    }

    /// <summary>
    ///     Returns a string with the specified text surrounded by the markdown underline specifier.
    /// </summary>
    /// <param name="value">The value to encapsulate as underline.</param>
    /// <returns>
    ///     <paramref name="value" /> surrounded by <c>__</c>, or <see langword="null" /> if <paramref name="value" /> is
    ///     <see langword="null" />.
    /// </returns>
    [return: NotNullIfNotNull("value")]
    public static string? Underline(string? value)
    {
        return value is null ? null : $"__{value}__";
    }
}
