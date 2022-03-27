using HeroWars.Hero.Counter.Bot.Data;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace HeroWars.Hero.Counter.Bot.Repositories;

class HeroRepository : IHeroRepository
{
    readonly string _connectionString;
    readonly string _databaseName;
    readonly string _containerName;

    public HeroRepository(IOptions<BotOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
        _databaseName = options.Value.DatabaseName;
        _containerName = options.Value.ContainerName;
    }

    public async Task<Document?> GetHeroCountersAsync(string heroName)
    {
        using var client = new CosmosClient(_connectionString);

        var database = await client
            .CreateDatabaseIfNotExistsAsync(_databaseName)
            .ConfigureAwait(false);

        var container = await database
            .Database
            .CreateContainerIfNotExistsAsync(_containerName, "/id")
            .ConfigureAwait(false);

        var query = new QueryDefinition($"SELECT * FROM c WHERE c.id = '{heroName}'");

        var resultSet = container.Container.GetItemQueryIterator<Document>(query);

        if (resultSet.HasMoreResults)
        {
            return (await resultSet
                .ReadNextAsync()
                .ConfigureAwait(false))
                .First();
        }

        return null;
    }
}
