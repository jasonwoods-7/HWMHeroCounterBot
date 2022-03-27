using System.Text;
using Discord;
using Discord.Interactions;
using HeroWars.Hero.Counter.Bot.AutoComplete;
using HeroWars.Hero.Counter.Bot.Repositories;

namespace HeroWars.Hero.Counter.Bot.Commands;

public class CounterCommand : InteractionModuleBase
{
    readonly IHeroRepository _repository;

    public CounterCommand(
        IHeroRepository repository) =>
        _repository = repository;

    [SlashCommand("counter", "Get a list of heroes which counter the specified hero")]
    public async Task CounterAsync(
        [Summary(description: "The hero you want to counter"), Autocomplete(typeof(HeroAutoComplete))] string heroName)
    {
        var hero = await _repository
            .GetHeroCountersAsync(heroName.ToLower())
            .ConfigureAwait(false);

        if (hero is null)
        {
            await RespondAsync(embed: new EmbedBuilder()
                    .WithTitle("Hero not found")
                    .WithDescription($"Hero {heroName} was not found. Run the /list-heroes commands for all available heroes.")
                    .Build(),
                ephemeral: true)
                .ConfigureAwait(false);

            return;
        }

        var hardCounters = ConcatCounters(hero.Counters.Hard);

        await RespondAsync(embed: new EmbedBuilder()
            .WithTitle($"Hard Counters for {hero.Hero.Name}")
            .WithDescription(hardCounters)
            .Build())
            .ConfigureAwait(false);

        var softCounters = ConcatCounters(hero.Counters.Soft);

        await ReplyAsync(embed: new EmbedBuilder()
            .WithTitle($"Soft Counters for {hero.Hero.Name}")
            .WithDescription(softCounters)
            .Build())
            .ConfigureAwait(false);
    }

    static string ConcatCounters(IReadOnlyList<string> counters) => counters
        .Aggregate(new StringBuilder(), (b, c) =>
        {
            if (b.Length != 0)
            {
                b.AppendLine().AppendLine();
            }

            return b.Append(c);
        }, b => b.ToString());
}
