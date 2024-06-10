using MediatR;
using ServerMonitoring.Application.LCDDisplay.Commands;

namespace ServerMonitoring.WorkerService.WorkerServices;

public class LcdDisplayWorkerService(
    ILogger<LcdDisplayWorkerService> logger,
    ISender mediator)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await mediator.Send(new ShowCurrentTimeCommand(), stoppingToken);
            await Task.Delay(5000, stoppingToken);
        }
    }
}