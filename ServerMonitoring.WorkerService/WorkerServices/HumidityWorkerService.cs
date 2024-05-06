using MediatR;
using ServerMonitoring.Application;
using ServerMonitoring.Application.BrickletHumidityV2.Queries;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.WorkerService.WorkerServices;

public class HumidityWorkerService(
    ILogger<HumidityWorkerService> logger,
    ISender mediator,
    MailService mailService) : BackgroundService
{
    private HumidityStatus? _lastStatus;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await mediator.Send(new GetHumidityQuery(), stoppingToken);

            if (_lastStatus != response.Status)
            {
                switch (response.Status)
                {
                    case HumidityStatus.High:
                        logger.LogWarning("Humidity is high ${Humidity}", response.Humidity);
                        mailService.SendMail("Humidity is high",
                            $"The humidity reached a high level. Please check the server as soon as possible. Humidity {response.Humidity}%");
                        break;
                    case HumidityStatus.Low:
                        logger.LogWarning("Humidity is low ${Humidity}", response.Humidity);
                        mailService.SendMail("Humidity is low",
                            $"The humidity reached a low level. Please check the server as soon as possible. Humidity {response.Humidity}%");
                        break;
                    case HumidityStatus.Normal:
                    default:
                        logger.LogInformation("Humidity is normal");
                        break;
                }

                _lastStatus = response.Status;
            }


            await Task.Delay(1000, stoppingToken);
        }
    }
}