namespace HeroWars.Hero.Counter.Bot;

public record BotOptions
(
    string Token,
    ulong GuildId,
    string ConnectionString,
    string DatabaseName,
    string ContainerName
)
{
    public BotOptions()
        : this(string.Empty, 0, string.Empty, string.Empty, string.Empty)
    {
    }
}
