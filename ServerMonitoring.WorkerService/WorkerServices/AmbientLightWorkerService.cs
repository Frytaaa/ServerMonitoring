using MediatR;
using ServerMonitoring.Application;
using ServerMonitoring.Application.AmbientLightBricklet.Queries;
using ServerMonitoring.Application.Responses;
using Tinkerforge;

namespace ServerMonitoring.WorkerService.WorkerServices;

public class AmbientLightWorkerService(
    ILogger<AmbientLightWorkerService> logger,
    ISender mediator,
    BrickletPiezoSpeakerV2 brickletPiezoSpeakerV2,
    MailService mailService)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await mediator.Send(new GetAmbientQuery(), stoppingToken);
            
            switch (response.Status)
            {
                case AmbientLightStatus.High:
                    logger.LogWarning("Ambient light is high ${AmbientLight}", response.AmbientLight);
                    mailService.SendMail("Ambient light is high",
                        "The ambient light reached a high level. Please check the server as soon as possible.");
                    break;
                case AmbientLightStatus.Low:
                    logger.LogWarning("Ambient light is low ${AmbientLight}", response.AmbientLight);
                    mailService.SendMail("Ambient light is low",
                        "The ambient light reached a low level. Please check the server as soon as possible.");
                    break;
                case AmbientLightStatus.Normal:
                default:
                    logger.LogInformation("Ambient light is normal");
                    break;
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    
}