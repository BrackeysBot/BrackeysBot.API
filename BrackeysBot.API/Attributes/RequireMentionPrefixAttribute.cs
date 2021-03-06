using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace BrackeysBot.API.Attributes;

/// <summary>
///     Defines that usage of this command must require a bot mention prefix rather than the plugin's defined prefix.
/// </summary>
public sealed class RequireMentionPrefixAttribute : CheckBaseAttribute
{
    /// <inheritdoc />
    public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
    {
        return Task.FromResult(MentionUtility.TryParseUser(ctx.Prefix[..^1], out ulong id) && id == ctx.Client.CurrentUser.Id);
    }
}
