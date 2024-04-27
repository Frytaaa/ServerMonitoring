using Tinkerforge;

namespace ServerMonitoring.WorkerService.Extensions;

public class TinkerforgeConnectionHostedService(IPConnection ipConnection, IConfiguration configuration)
    : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        ipConnection.Connect(configuration.GetSection("Tinkerforge")["Host"],
            int.Parse(configuration.GetSection("Tinkerforge")["Port"]));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        ipConnection.Disconnect();
        return Task.CompletedTask;
    }
}