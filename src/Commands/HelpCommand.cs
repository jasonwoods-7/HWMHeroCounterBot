using Discord;
using Discord.Interactions;

namespace HeroWars.Hero.Counter.Bot.Commands;

public class HelpCommand : InteractionModuleBase
{
    [SlashCommand("help", "Show a list of all commands")]
    public Task HelpAsync() =>
        RespondAsync(embed: new EmbedBuilder()
            .WithTitle("HWM Hero Counter Bot Help")
            .WithDescription(@"/list-heroes - List all heroes recognized by this bot.
/counter {hero-name} - Get a list of heroes which counter the specified hero.
/help - Show this message.
/about - Info about this bot.")
            .Build());
}
