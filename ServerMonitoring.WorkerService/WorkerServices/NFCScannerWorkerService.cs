using MediatR;
using ServerMonitoring.Application;
using ServerMonitoring.Application.NFCScanner.Queries;
using ServerMonitoring.Application.LEDButtonBricklet.Queries;
using ServerMonitoring.Application.Responses;
using Tinkerforge;

namespace ServerMonitoring.WorkerService.WorkerServices;

public class NFCScannerWorkerService(
    ILogger<NFCScannerWorkerService> logger,
    ISender mediator,
    MailService mailService)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var nfcresponse = await mediator.Send(new GetNFCScannerQuery(), stoppingToken);
            LEDButtonStatus ledStatus;

            switch (nfcresponse.Status)
            {
                case NFCScannerStatus.Ready:
                    ledStatus = LEDButtonStatus.Blue;
                    break;
                case NFCScannerStatus.OK:
                    ledStatus = LEDButtonStatus.Green;
                    break;
                case NFCScannerStatus.NotOk:
                    ledStatus = LEDButtonStatus.Red;
                    break;
                default:
                    ledStatus = LEDButtonStatus.Blue;
                    break;
            }
            await mediator.Send(new LEDButtonResponse { Status = ledStatus }, stoppingToken);
            await Task.Delay(3000, stoppingToken);
            await mediator.Send(new LEDButtonResponse { Status = LEDButtonStatus.Blue }, stoppingToken);
        }
    }
}
