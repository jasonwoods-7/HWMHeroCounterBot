namespace HeroWars.Hero.Counter.Bot;

public record BotOptions
(
    string Token,
    IReadOnlyList<ulong> GuildIds
)
{
    public BotOptions()
        : this(string.Empty, Array.Empty<ulong>())
    {
    }
}
