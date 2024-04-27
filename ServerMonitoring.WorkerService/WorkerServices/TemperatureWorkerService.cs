using MediatR;
using ServerMonitoring.Application;
using ServerMonitoring.Application.BrickletPTCV2.Queries;
using ServerMonitoring.Application.Responses;
using Tinkerforge;

namespace ServerMonitoring.WorkerService.WorkerServices;

public class TemperatureWorkerService(
    ILogger<TemperatureWorkerService> logger,
    ISender mediator,
    BrickletPiezoSpeakerV2 brickletPiezoSpeakerV2,
    MailService mailService)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await mediator.Send(new GetTemperatureQuery(), stoppingToken);
            // TODO mails are sent at every loop iteration, change it to send only once and maybe one more time after a certain time
            switch (response.Status)
            {
                case TemperatureStatus.Critical:
                    logger.LogCritical("Temperature is critical ${Temperature}", response.Temperature);
                    mailService.SendMail("Temperature is critical",
                        "The temperature reached a critical level. Please check the server immediately.");
                    brickletPiezoSpeakerV2.SetAlarm(1000, 5000, 3, 2, 1, 2);
                    break;
                case TemperatureStatus.High:
                case TemperatureStatus.Low:
                    logger.LogWarning("Temperature is outside the normal value ${Temperature}", response.Temperature);
                    mailService.SendMail("The temperature is outside the normal value.",
                        "Please check the server as soon as possible");
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