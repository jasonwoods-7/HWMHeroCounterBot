using System.Reflection;
using Discord.Interactions;
using EnumFastToStringGenerated;

namespace HeroWars.Hero.Counter.Bot.Data;

public static class HeroExtensions
{
    public static string GetHeroName(this Hero hero)
    {
        var heroName = hero.FastToString();

        return typeof(Hero)
                .GetMember(heroName)[0]
                .GetCustomAttribute<ChoiceDisplayAttribute>()
                ?.Name
            ?? heroName;
    }
}
