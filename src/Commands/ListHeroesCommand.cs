using System.Text;
using Discord;
using Discord.Interactions;
using HeroWars.Hero.Counter.Bot.Extension;

namespace HeroWars.Hero.Counter.Bot.Commands;

public class ListHeroesCommand : InteractionModuleBase
{
    [SlashCommand("list-heroes", "List all heroes recognized by this bot")]
    public Task ListHeroesAsync()
    {
        var messageBuilder = new StringBuilder();
        foreach (var hero in Enum.GetValues<Data.Hero>())
        {
            messageBuilder.AppendLine(hero.GetHeroName());
        }

        return RespondAsync(embed: new EmbedBuilder()
            .WithTitle("Heroes")
            .WithDescription(messageBuilder.ToString())
            .Build());
    }
}
