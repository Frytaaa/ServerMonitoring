using MediatR;
using ServerMonitoring.Application.BrickletPTCV2.Queries;
using ServerMonitoring.Application.Responses;
using Tinkerforge;

namespace ServerMonitoring.WorkerService.WorkerServices;

public class TemperatureWorkerService(
    ILogger<TemperatureWorkerService> logger,
    IMediator mediator,
    BrickletPTCV2 brickletPtcv2,
    BrickletPiezoSpeakerV2 brickletPiezoSpeakerV2)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await mediator.Send(new GetTemperatureQuery { Device = brickletPtcv2 }, stoppingToken);
        
            switch (response.Status)
            {
                case TemperatureStatus.High:
                case TemperatureStatus.Low:
                    brickletPiezoSpeakerV2.SetAlarm(1000, 5000, 3, 2, 1, 2);
                    break;
                case TemperatureStatus.Normal:
                default:
                    logger.LogInformation("Everything fine");
                    break;
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}