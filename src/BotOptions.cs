namespace HeroWars.Hero.Counter.Bot;

public record BotOptions
(
    string Token,
    ulong GuildId
)
{
    public BotOptions()
        : this(string.Empty, 0)
    {
    }
}
