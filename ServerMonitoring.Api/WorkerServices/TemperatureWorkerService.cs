using MediatR;
using ServerMonitoring.Application;
using ServerMonitoring.Application.BrickletPTCV2.Queries;
using ServerMonitoring.Application.Responses;
using ServerMonitoring.Application.SegmentDisplay.Commands;
using Tinkerforge;

namespace ServerMonitoring.Api.WorkerServices;

public class TemperatureWorkerService(
    ILogger<TemperatureWorkerService> logger,
    ISender mediator,
    BrickletPiezoSpeakerV2 speaker,
    MailService mailService)
    : BackgroundService
{
    private TemperatureStatus? _lastStatus;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var temperatureResponse = await mediator.Send(new GetTemperatureQuery(), stoppingToken);
            await mediator.Send(new SetSegmentsCommand { Temperature = temperatureResponse.Temperature },
                stoppingToken);

            if (_lastStatus != temperatureResponse.Status)
            {
                switch (temperatureResponse.Status)
                {
                    case TemperatureStatus.Critical:
                        logger.LogCritical("Temperature is critical ${Temperature}", temperatureResponse.Temperature);
                        mailService.SendMail("Temperature is critical",
                            $"The temperature reached a critical level. Please check the server immediately. Temperature {temperatureResponse.Temperature}°C");
                        speaker.SetAlarm(800, 2000, 10, 1, 5, 2);
                        break;
                    case TemperatureStatus.High:
                    case TemperatureStatus.Low:
                        logger.LogWarning("Temperature is outside the normal value {Temperature}", temperatureResponse.Temperature);
                        mailService.SendMail("The temperature is outside the normal value.",
                            $"Please check the server as soon as possible. Temperature {temperatureResponse.Temperature} °C");
                        break;
                    case TemperatureStatus.Normal:
                    default:
                        logger.LogInformation("Everything fine");
                        break;
                }

                _lastStatus = temperatureResponse.Status;
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}