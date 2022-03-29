using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Options;

namespace HeroWars.Hero.Counter.Bot;

public class Worker : BackgroundService
{
    readonly IServiceProvider _services;
    readonly DiscordSocketClient _client;
    readonly InteractionService _interactionService;
    readonly BotOptions _botOptions;
    readonly ILogger<Worker> _logger;

    public Worker(
        IServiceProvider services,
        DiscordSocketClient client,
        IOptions<BotOptions> botOptions,
        ILogger<Worker> logger)
    {
        _services = services;
        _client = client;
        _client.Log += ClientOnLogAsync;
        _client.Ready += ClientOnReadyAsync;
        _client.InteractionCreated += ClientOnInteractionCreatedAsync;
        _interactionService = new InteractionService(_client.Rest);
        _interactionService.Log += ClientOnLogAsync;
        _botOptions = botOptions.Value;
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

    async Task ClientOnReadyAsync()
    {
        await _interactionService
            .AddModulesAsync(typeof(Worker).Assembly, _services)
            .ConfigureAwait(false);

#if DEBUG || DEBUG___COSMOS
        var guildId = _botOptions.GuildId;

        await _interactionService
            .RegisterCommandsToGuildAsync(guildId)
            .ConfigureAwait(false);
#else
        await _interactionService
            .RegisterCommandsGloballyAsync()
            .ConfigureAwait(false);
#endif
    }

    Task ClientOnInteractionCreatedAsync(SocketInteraction arg) =>
        _interactionService.ExecuteCommandAsync(new SocketInteractionContext(_client, arg), _services);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var token = _botOptions.Token;

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

        _interactionService.Dispose();

        await _client
            .LogoutAsync()
            .ConfigureAwait(false);

        await _client
            .StopAsync()
            .ConfigureAwait(false);

        await _client
            .DisposeAsync()
            .ConfigureAwait(false);
    }
}
