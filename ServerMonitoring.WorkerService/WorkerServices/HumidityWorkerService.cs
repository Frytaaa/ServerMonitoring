using MediatR;
using ServerMonitoring.Application.BrickletHumidityV2.Queries;
using ServerMonitoring.Application.Responses;
using Tinkerforge;

namespace ServerMonitoring.WorkerService.WorkerServices;

public class HumidityWorkerService(
    ILogger<HumidityWorkerService> logger,
    IMediator mediator,
    BrickletHumidityV2 humidityV2) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await mediator.Send(new GetHumidityQuery { Device = humidityV2 }, stoppingToken);

            switch (response.Status)
            {
                case HumidityStatus.High: 
                    // do something
                    break;
                case HumidityStatus.Normal:
                case HumidityStatus.Low:
                default:
                    logger.LogInformation("Everything fine");
                    break;
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}