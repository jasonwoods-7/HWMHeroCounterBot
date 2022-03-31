using System.Reflection;
using Discord.Interactions;
using EnumFastToStringGenerated;

namespace HeroWars.Hero.Counter.Bot.Extension;

public static class HeroExtensions
{
    public static string GetHeroName(this Data.Hero hero)
    {
        var heroName = hero.FastToString();

        return typeof(Data.Hero)
                .GetMember(heroName)[0]
                .GetCustomAttribute<ChoiceDisplayAttribute>()
                ?.Name
            ?? heroName;
    }
}
