using Discord;
using Discord.WebSocket;

namespace HeroWars.Hero.Counter.Bot;

public class Worker : BackgroundService
{
    readonly IConfiguration _config;
    readonly DiscordSocketClient _client;
    readonly ILogger<Worker> _logger;

    public Worker(
        IConfiguration config,
        DiscordSocketClient client,
        ILogger<Worker> logger)
    {
        _config = config;
        _client = client;
        _client.Log += ClientOnLogAsync;
        _logger = logger;
    }

    Task ClientOnLogAsync(LogMessage arg)
    {
        Action<Exception, string, object[]> handler = arg.Severity switch
        {
            LogSeverity.Critical => _logger.LogCritical,
            LogSeverity.Error => _logger.LogError,
            LogSeverity.Warning => _logger.LogWarning,
            LogSeverity.Info => _logger.LogInformation,
            LogSeverity.Verbose => _logger.LogTrace,
            _ => _logger.LogDebug
        };

        handler(arg.Exception, arg.Message, Array.Empty<object>());

        return Task.CompletedTask;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var token = _config.GetValue<string>("Discord:Token");

        await _client
            .LoginAsync(TokenType.Bot, token)
            .ConfigureAwait(false);

        await _client
            .StartAsync()
            .ConfigureAwait(false);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base
            .StopAsync(cancellationToken)
            .ConfigureAwait(false);

        await _client
            .LogoutAsync()
            .ConfigureAwait(false);

        await _client
            .StopAsync()
            .ConfigureAwait(false);
    }
}
