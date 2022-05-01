using System;
using Remora.Discord.API;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.Extensions.Embeds;

namespace BrackeysBot.API.Extensions;

/// <summary>
///     Extension methods for <see cref="EmbedBuilder" />.
/// </summary>
public static class EmbedBuilderExtensions
{
    /// <summary>
    ///     Adds a field of any value to an embed.
    /// </summary>
    /// <param name="embedBuilder">The <see cref="EmbedBuilder" /> to modify.</param>
    /// <param name="name">The field name.</param>
    /// <param name="value">The field value.</param>
    /// <param name="inline"><see langword="true" /> to display the field inline; otherwise, <see langword="false" />.</param>
    /// <typeparam name="T">The value type of the field.</typeparam>
    /// <returns><paramref name="embedBuilder" />, to allow for method chaining.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static EmbedBuilder AddField<T>(this EmbedBuilder embedBuilder, string name, T value, bool inline = false)
    {
        if (value is null) throw new ArgumentNullException(nameof(value));
        return embedBuilder.AddField(name, value.ToString()!, inline).Entity;
    }

    /// <summary>
    ///     Adds a field of any value to an embed.
    /// </summary>
    /// <param name="embedBuilder">The <see cref="EmbedBuilder" /> to modify.</param>
    /// <param name="name">The field name.</param>
    /// <param name="valueEvaluator">The delegate to execute to retrieve the field value.</param>
    /// <param name="inline"><see langword="true" /> to display the field inline; otherwise, <see langword="false" />.</param>
    /// <typeparam name="T">The value type of the field.</typeparam>
    /// <returns><paramref name="embedBuilder" />, to allow for method chaining.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static EmbedBuilder AddField<T>(this EmbedBuilder embedBuilder, string name, Func<T> valueEvaluator,
        bool inline = false)
    {
        if (valueEvaluator is null) throw new ArgumentNullException(nameof(valueEvaluator));
        if (valueEvaluator()?.ToString() is not { } value) throw new ArgumentNullException(nameof(valueEvaluator));
        return embedBuilder.AddField(name, value, inline).Entity;
    }

    /// <summary>
    ///     Conditionally adds a field to an embed.
    /// </summary>
    /// <param name="embedBuilder">The <see cref="EmbedBuilder" /> to modify.</param>
    /// <param name="conditionEvaluator">
    ///     A delegate which returns <see langword="true" /> if the field should be added; otherwise, returns
    ///     <see langword="false" />.
    /// </param>
    /// <param name="name">The field name.</param>
    /// <param name="value">The field value.</param>
    /// <param name="inline"><see langword="true" /> to display the field inline; otherwise, <see langword="false" />.</param>
    /// <typeparam name="T">The value type of the field.</typeparam>
    /// <returns><paramref name="embedBuilder" />, to allow for method chaining.</returns>
    public static EmbedBuilder AddFieldIf<T>(this EmbedBuilder embedBuilder, Func<bool> conditionEvaluator,
        string name, T value, bool inline = false)
    {
        if (conditionEvaluator is null) throw new ArgumentNullException(nameof(conditionEvaluator));
        if (!conditionEvaluator()) return embedBuilder;
        return embedBuilder.AddField(name, value, inline);
    }

    /// <summary>
    ///     Conditionally adds a field to an embed.
    /// </summary>
    /// <param name="embedBuilder">The <see cref="EmbedBuilder" /> to modify.</param>
    /// <param name="conditionEvaluator">
    ///     A delegate which returns <see langword="true" /> if the field should be added; otherwise, returns
    ///     <see langword="false" />.
    /// </param>
    /// <param name="name">The field name.</param>
    /// <param name="valueEvaluator">The delegate to execute if <paramref name="conditionEvaluator" /> is <see langword="true" />.</param>
    /// <param name="inline"><see langword="true" /> to display the field inline; otherwise, <see langword="false" />.</param>
    /// <typeparam name="T">The value type of the field.</typeparam>
    /// <returns><paramref name="embedBuilder" />, to allow for method chaining.</returns>
    public static EmbedBuilder AddFieldIf<T>(this EmbedBuilder embedBuilder, Func<bool> conditionEvaluator,
        string name, Func<T> valueEvaluator, bool inline = false)
    {
        if (valueEvaluator is null) throw new ArgumentNullException(nameof(valueEvaluator));
        if (!conditionEvaluator()) return embedBuilder;
        return embedBuilder.AddField(name, valueEvaluator(), inline);
    }

    /// <summary>
    ///     Conditionally adds a field to an embed, lazily.
    /// </summary>
    /// <param name="embedBuilder">The <see cref="EmbedBuilder" /> to modify.</param>
    /// <param name="condition"><see langword="true" /> if the field should be added; otherwise, <see langword="false" />.</param>
    /// <param name="name">The field name.</param>
    /// <param name="valueEvaluator">The delegate to execute if <paramref name="condition" /> is <see langword="true" />.</param>
    /// <param name="inline"><see langword="true" /> to display the field inline; otherwise, <see langword="false" />.</param>
    /// <typeparam name="T">The value type of the field.</typeparam>
    /// <returns><paramref name="embedBuilder" />, to allow for method chaining.</returns>
    public static EmbedBuilder AddFieldIf<T>(this EmbedBuilder embedBuilder, bool condition, string name,
        Func<T> valueEvaluator, bool inline = false)
    {
        if (valueEvaluator is null) throw new ArgumentNullException(nameof(valueEvaluator));
        if (!condition) return embedBuilder;
        return embedBuilder.AddField(name, valueEvaluator(), inline);
    }

    /// <summary>
    ///     Conditionally adds a field to an embed.
    /// </summary>
    /// <param name="embedBuilder">The <see cref="EmbedBuilder" /> to modify.</param>
    /// <param name="condition"><see langword="true" /> if the field should be added; otherwise, <see langword="false" />.</param>
    /// <param name="name">The field name.</param>
    /// <param name="value">The field value.</param>
    /// <param name="inline"><see langword="true" /> to display the field inline; otherwise, <see langword="false" />.</param>
    /// <typeparam name="T">The value type of the field.</typeparam>
    /// <returns><paramref name="embedBuilder" />, to allow for method chaining.</returns>
    public static EmbedBuilder AddFieldIf<T>(this EmbedBuilder embedBuilder, bool condition, string name, T value,
        bool inline = false)
    {
        if (!condition) return embedBuilder;
        return embedBuilder.AddField(name, value, inline);
    }

    /// <summary>
    ///     Adds a field to an embed whose value will be one of two values, determined by a specified condition.
    /// </summary>
    /// <param name="embedBuilder">The <see cref="EmbedBuilder" /> to modify.</param>
    /// <param name="condition">
    ///     <see langword="true" /> if the value used will be <paramref name="valueIfTrue" />.
    ///     <see langword="false" /> if the value used will be <paramref name="valueIfFalse" />.
    /// </param>
    /// <param name="name">The field name.</param>
    /// <param name="valueIfTrue">
    ///     The value to be used as the field value if <paramref name="condition" /> is <see langword="true" />.
    /// </param>
    /// <param name="valueIfFalse">
    ///     The value to be used as the field value if <paramref name="condition" /> is <see langword="false" />.
    /// </param>
    /// <param name="inline"><see langword="true" /> to display the field inline; otherwise, <see langword="false" />.</param>
    /// <typeparam name="TTrue">The type of the truth value.</typeparam>
    /// <typeparam name="TFalse">The type of the false value.</typeparam>
    /// <returns><paramref name="embedBuilder" />, to allow for method chaining.</returns>
    public static EmbedBuilder AddFieldOrElse<TTrue, TFalse>(this EmbedBuilder embedBuilder, bool condition,
        string name, TTrue valueIfTrue, TFalse valueIfFalse, bool inline = false)
    {
        return condition
            ? embedBuilder.AddField(name, valueIfTrue, inline)
            : embedBuilder.AddField(name, valueIfFalse, inline);
    }

    /// <summary>
    ///     Adds a field to an embed whose value will be one of two values, determined by a specified condition.
    /// </summary>
    /// <param name="embedBuilder">The <see cref="EmbedBuilder" /> to modify.</param>
    /// <param name="condition">
    ///     <see langword="true" /> if the value used will be determined by <paramref name="valueEvaluatorIfTrue" />; or returns
    ///     <see langword="false" /> if the value used will be determined by <paramref name="valueEvaluatorIfFalse" />.
    /// </param>
    /// <param name="name">The field name.</param>
    /// <param name="valueEvaluatorIfTrue">
    ///     The delegate whose return value is used if <paramref name="condition" /> is <see langword="true" />.
    /// </param>
    /// <param name="valueEvaluatorIfFalse">
    ///     The delegate whose return value is used if <paramref name="condition" /> is <see langword="false" />.
    /// </param>
    /// <param name="inline"><see langword="true" /> to display the field inline; otherwise, <see langword="false" />.</param>
    /// <typeparam name="TTrue">The type of the truth value.</typeparam>
    /// <typeparam name="TFalse">The type of the false value.</typeparam>
    /// <returns><paramref name="embedBuilder" />, to allow for method chaining.</returns>
    public static EmbedBuilder AddFieldOrElse<TTrue, TFalse>(this EmbedBuilder embedBuilder, bool condition,
        string name, Func<TTrue> valueEvaluatorIfTrue, Func<TFalse> valueEvaluatorIfFalse, bool inline = false)
    {
        if (valueEvaluatorIfTrue is null) throw new ArgumentNullException(nameof(valueEvaluatorIfTrue));
        if (valueEvaluatorIfFalse is null) throw new ArgumentNullException(nameof(valueEvaluatorIfFalse));

        return condition
            ? embedBuilder.AddField(name, valueEvaluatorIfTrue(), inline)
            : embedBuilder.AddField(name, valueEvaluatorIfFalse(), inline);
    }

    /// <summary>
    ///     Adds a field to an embed whose value will be one of two values, determined by a specified condition.
    /// </summary>
    /// <param name="embedBuilder">The <see cref="EmbedBuilder" /> to modify.</param>
    /// <param name="conditionEvaluator">
    ///     A delegate which returns <see langword="true" /> if the value used will be determined by
    ///     <paramref name="valueEvaluatorIfTrue" />; or returns <see langword="false" />  if the value used will be determined by
    ///     <paramref name="valueEvaluatorIfFalse" />.
    /// </param>
    /// <param name="name">The field name.</param>
    /// <param name="valueEvaluatorIfTrue">
    ///     The delegate whose return value is used as the field value if the value returned by
    ///     <paramref name="conditionEvaluator" /> is <see langword="true" />.
    /// </param>
    /// <param name="valueEvaluatorIfFalse">
    ///     The delegate whose return value is used as the field value if the value returned by
    ///     <paramref name="conditionEvaluator" /> is <see langword="false" />.
    /// </param>
    /// <param name="inline"><see langword="true" /> to display the field inline; otherwise, <see langword="false" />.</param>
    /// <typeparam name="TTrue">The type of the truth value.</typeparam>
    /// <typeparam name="TFalse">The type of the false value.</typeparam>
    /// <returns><paramref name="embedBuilder" />, to allow for method chaining.</returns>
    public static EmbedBuilder AddFieldOrElse<TTrue, TFalse>(this EmbedBuilder embedBuilder,
        Func<bool> conditionEvaluator, string name, Func<TTrue> valueEvaluatorIfTrue, Func<TFalse> valueEvaluatorIfFalse,
        bool inline = false)
    {
        if (conditionEvaluator is null) throw new ArgumentNullException(nameof(conditionEvaluator));
        if (valueEvaluatorIfTrue is null) throw new ArgumentNullException(nameof(valueEvaluatorIfTrue));
        if (valueEvaluatorIfFalse is null) throw new ArgumentNullException(nameof(valueEvaluatorIfFalse));

        return conditionEvaluator()
            ? embedBuilder.AddField(name, valueEvaluatorIfTrue(), inline)
            : embedBuilder.AddField(name, valueEvaluatorIfFalse(), inline);
    }

    /// <summary>
    ///     Adds a field to an embed whose value will be one of two values, determined by a specified condition.
    /// </summary>
    /// <param name="embedBuilder">The <see cref="EmbedBuilder" /> to modify.</param>
    /// <param name="conditionEvaluator">
    ///     A delegate which returns <see langword="true" /> if the value used will be <paramref name="valueIfTrue" />; or returns
    ///     <see langword="false" />  if the value used will be <paramref name="valueIfFalse" />.
    ///     .
    /// </param>
    /// <param name="name">The field name.</param>
    /// <param name="valueIfTrue">
    ///     The delegate whose return value will be used as the field value if the value returned by
    ///     <paramref name="conditionEvaluator" /> is <see langword="true" />.
    /// </param>
    /// <param name="valueIfFalse">
    ///     The delegate whose return value will be used as the field value if the value returned by
    ///     <paramref name="conditionEvaluator" /> is <see langword="false" />.
    /// </param>
    /// <param name="inline"><see langword="true" /> to display the field inline; otherwise, <see langword="false" />.</param>
    /// <typeparam name="TTrue">The type of the truth value.</typeparam>
    /// <typeparam name="TFalse">The type of the false value.</typeparam>
    /// <returns><paramref name="embedBuilder" />, to allow for method chaining.</returns>
    public static EmbedBuilder AddFieldOrElse<TTrue, TFalse>(this EmbedBuilder embedBuilder,
        Func<bool> conditionEvaluator, string name, TTrue valueIfTrue, TFalse valueIfFalse, bool inline = false)
    {
        if (conditionEvaluator is null) throw new ArgumentNullException(nameof(conditionEvaluator));

        return conditionEvaluator()
            ? embedBuilder.AddField(name, valueIfTrue, inline)
            : embedBuilder.AddField(name, valueIfFalse, inline);
    }

    /// <summary>
    ///     Populates the thumbnail and footer of this embed builder with the guild's branding.
    /// </summary>
    /// <param name="embedBuilder">The <see cref="EmbedBuilder" /> to modify.</param>
    /// <param name="guild">The guild whose branding to apply.</param>
    /// <param name="addThumbnail">
    ///     <see langword="true" /> to include the guild icon as a thumbnail; otherwise, <see langword="false" />.
    /// </param>
    /// <returns><paramref name="embedBuilder" />, to allow for method chaining.</returns>
    public static EmbedBuilder WithGuildInfo(this EmbedBuilder embedBuilder, IGuild guild,
        bool addThumbnail = true)
    {
        var iconUrl = CDN.GetGuildIconUrl(guild).ToString();
        embedBuilder.WithFooter(guild.Name, iconUrl);

        if (addThumbnail) embedBuilder.WithThumbnailUrl(iconUrl!);
        return embedBuilder;
    }
}
