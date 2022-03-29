using HeroWars.Hero.Counter.Bot.Data;
using Newtonsoft.Json;

namespace HeroWars.Hero.Counter.Bot.Repositories;

class FileHeroRepository : IHeroRepository
{
    public async Task<Document?> GetHeroCountersAsync(string heroName, CancellationToken cancellationToken = default)
    {
        var info = new FileInfo(Path.Combine(Environment.CurrentDirectory, "..", "data", $"{heroName}.json"));

        if (!info.Exists)
        {
            return null;
        }

        await using var data = info.OpenRead();
        using var reader = new StreamReader(data);

        return JsonConvert.DeserializeObject<Document>(await reader.ReadToEndAsync().ConfigureAwait(false));
    }
}
