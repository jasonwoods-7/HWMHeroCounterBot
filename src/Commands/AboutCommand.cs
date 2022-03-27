using System.Reflection;
using System.Text;
using Discord;
using Discord.Interactions;

namespace HeroWars.Hero.Counter.Bot.Commands;

public class AboutCommand : InteractionModuleBase
{
    [SlashCommand("about", "Info about this bot")]
    public Task AboutAsync()
    {
        static string GetAssemblyName()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();

            var name = new StringBuilder();

            if (assemblyName.Name is { } n)
            {
                name.Append(n);
            }
            else
            {
                name.Append("HWMHeroCounterBot");
            }

            if (assemblyName.Version is { } v)
            {
                name.AppendFormat(" - v.{0}", v);
            }

            return name.ToString();
        }

        return RespondAsync(embed: new EmbedBuilder()
            .WithTitle("About this bot")
            .WithDescription(@$"{GetAssemblyName()}
GitHub Project - https://github.com/jasonwoods-7/HWMHeroCounterBot")
            .Build());
    }
}
