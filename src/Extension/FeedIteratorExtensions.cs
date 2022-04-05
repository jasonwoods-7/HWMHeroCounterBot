using System.Runtime.CompilerServices;
using Microsoft.Azure.Cosmos;

namespace HeroWars.Hero.Counter.Bot.Extension;

public static class FeedIteratorExtensions
{
    public static async IAsyncEnumerable<T> ResultsAsync<T>(
        this FeedIterator<T> source,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        while (source.HasMoreResults)
        {
            var page = await source
                .ReadNextAsync(cancellationToken)
                .ConfigureAwait(false);

            foreach (var current in page)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return current;
            }
        }
    }
}
