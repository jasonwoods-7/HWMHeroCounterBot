using Discord;
using Discord.Interactions;
using HeroWars.Hero.Counter.Bot.AutoComplete;
using HeroWars.Hero.Counter.Bot.Data;

namespace HeroWars.Hero.Counter.Bot.Commands;

public class CounterCommand : InteractionModuleBase
{
    [SlashCommand("counter", "Get a list of heroes which counter the specified hero")]
    public Task CounterAsync(
        [Summary(description: "The hero you want to counter"), Autocomplete(typeof(HeroAutoComplete))] string heroName)
    {
        try
        {
            var hero = Enum.Parse<Data.Hero>(heroName, true);

            return RespondAsync(embed: new EmbedBuilder()
                .WithTitle($"Hero Counters for {hero.GetHeroName()}")
                .WithDescription("add hero counters here")
                .Build());
        }
        catch (ArgumentException)
        {
            return RespondAsync(embed: new EmbedBuilder()
                .WithTitle("Hero not found")
                .WithDescription($"Hero {heroName} was not found. Run the /list-heroes commands for all available heroes.")
                .Build(),
                ephemeral: true);
        }
    }
}
