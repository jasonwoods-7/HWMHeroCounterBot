using Discord.WebSocket;
using HeroWars.Hero.Counter.Bot.Repositories;
using Serilog;

namespace HeroWars.Hero.Counter.Bot;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            var host = Host
                .CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console(
                            outputTemplate: "{Timestamp:yyyy-MM-ddTHH:mm:ss} [{Level:u3}] - {SourceContext} - {Message:lj}{NewLine}{Exception}")
                        .CreateLogger();

                    services
                        .AddHostedService<Worker>()
                        .Configure<BotOptions>(hostContext.Configuration.GetSection(nameof(BotOptions)))
                        .Configure<CosmosOptions>(hostContext.Configuration.GetSection(nameof(CosmosOptions)))
                        .AddTransient<DiscordSocketClient>()
#if DEBUG
                        .AddTransient<IHeroRepository, FileHeroRepository>()
#else
                        .AddTransient<IHeroRepository, CosmosHeroRepository>()
#endif
                        ;
                })
                .UseSerilog()
                .Build();

            Log.ForContext(typeof(Program)).Information("Starting host...");

            await host
                .RunAsync()
                .ConfigureAwait(false);

            Log.ForContext(typeof(Program)).Information("Host stopped");

            return 0;
        }
        catch (Exception ex)
        {
            Log.ForContext(typeof(Program)).Fatal(ex, "Host terminated unexpectedly");
            return -1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
