namespace HeroWars.Hero.Counter.Bot;

public record CosmosOptions
(
    string ConnectionString,
    string DatabaseName,
    string ContainerName
)
{
    public CosmosOptions()
        : this(string.Empty, string.Empty, string.Empty)
    {
    }
}
