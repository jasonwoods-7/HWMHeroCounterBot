using HeroWars.Hero.Counter.Bot.Data;

namespace HeroWars.Hero.Counter.Bot.Repositories;

public interface IHeroRepository
{
    Task<Document?> GetHeroCountersAsync(string heroName);
}
