using MediatR;
using ServerMonitoring.Application;
using ServerMonitoring.Application.BrickletPTCV2.Queries;
using ServerMonitoring.Application.Responses;
using Tinkerforge;

namespace ServerMonitoring.WorkerService.WorkerServices;

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
            var response = await mediator.Send(new GetTemperatureQuery(), stoppingToken);
            if (_lastStatus != response.Status)
            {
                switch (response.Status)
                {
                    case TemperatureStatus.Critical:
                        logger.LogCritical("Temperature is critical ${Temperature}", response.Temperature);
                        mailService.SendMail("Temperature is critical",
                            $"The temperature reached a critical level. Please check the server immediately. Temperature {response.Temperature}°C");
                        speaker.SetAlarm(1000, 5000, 3, 2, 1, 2);
                        break;
                    case TemperatureStatus.High:
                    case TemperatureStatus.Low:
                        logger.LogWarning("Temperature is outside the normal value ${Temperature}",
                            response.Temperature);
                        mailService.SendMail("The temperature is outside the normal value.",
                            $"Please check the server as soon as possible. Temperature {response.Temperature} °C");
                        break;
                    case TemperatureStatus.Normal:
                    default:
                        logger.LogInformation("Everything fine");
                        break;
                }

                _lastStatus = response.Status;
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}