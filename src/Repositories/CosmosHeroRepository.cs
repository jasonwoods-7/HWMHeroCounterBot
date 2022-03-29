using HeroWars.Hero.Counter.Bot.Data;
using HeroWars.Hero.Counter.Bot.Extension;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace HeroWars.Hero.Counter.Bot.Repositories;

class CosmosHeroRepository : IHeroRepository
{
    readonly string _connectionString;
    readonly string _databaseName;
    readonly string _containerName;

    public CosmosHeroRepository(IOptions<CosmosOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
        _databaseName = options.Value.DatabaseName;
        _containerName = options.Value.ContainerName;
    }

    public async Task<Document?> GetHeroCountersAsync(string heroName, CancellationToken cancellationToken = default)
    {
        using var client = new CosmosClient(_connectionString);

        var container = client.GetContainer(_databaseName, _containerName);

        using var resultSet = container.GetItemQueryIterator<Document>($"SELECT * FROM c WHERE c.id = '{heroName}'");

        return await resultSet
            .ResultsAsync(cancellationToken)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
