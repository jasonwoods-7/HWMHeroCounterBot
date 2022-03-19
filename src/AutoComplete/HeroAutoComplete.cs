using Discord;
using Discord.Interactions;
using Gma.DataStructures.StringSearch;
using HeroWars.Hero.Counter.Bot.Data;

namespace HeroWars.Hero.Counter.Bot.AutoComplete;

public class HeroAutoComplete : AutocompleteHandler
{
    readonly ITrie<Data.Hero> _heroesTrie;

    public HeroAutoComplete()
    {
        _heroesTrie = new Trie<Data.Hero>();

        foreach (var hero in Enum.GetValues<Data.Hero>())
        {
            _heroesTrie.Add(hero.GetHeroName(), hero);
        }
    }

    public override Task<AutocompletionResult> GenerateSuggestionsAsync(
        IInteractionContext context,
        IAutocompleteInteraction autocompleteInteraction,
        IParameterInfo parameter,
        IServiceProvider services)
    {
        var suggestions = _heroesTrie
            .Retrieve((string)autocompleteInteraction.Data.Options.First().Value)
            .Take(15)
            .Select(h => new AutocompleteResult(h.GetHeroName(), h.ToString()))
            .ToList();

        return Task.FromResult(AutocompletionResult.FromSuccess(suggestions));
    }
}
