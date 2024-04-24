using Tinkerforge;

namespace ServerMonitoring.WorkerService.Extensions;

public class TinkerforgeConnectionHostedService : IHostedService
{
    private readonly IPConnection _ipConnection;
    private readonly IConfiguration _configuration;

    public TinkerforgeConnectionHostedService(IPConnection ipConnection, IConfiguration configuration)
    {
        _ipConnection = ipConnection;
        _configuration = configuration;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _ipConnection.Connect(_configuration.GetSection("Tinkerforge")["Host"],
            int.Parse(_configuration.GetSection("Tinkerforge")["Port"]));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _ipConnection.Disconnect();
        return Task.CompletedTask;
    }
}