using MediatR;
using ServerMonitoring.Application.BrickletMotionDetectorV2.Queries;
using ServerMonitoring.Application.Responses;
using Tinkerforge;

namespace ServerMonitoring.WorkerService.WorkerServices;

public class TemplateWorkerService(
ILogger<MotionWorkerService> logger,
IMediator mediator
BrickletMotionDetectorV2 motionV2)
: BackgroundService
{
    //private readonly ILogger<MotionWorkerService> _logger = logger;
    //private readonly IMediator _mediator = mediator;


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
       while (!stoppingToken.IsCancellationRequested)
               {
                   var response = await mediator.Send(new GetMotionQuery { Device = motionV2 }, stoppingToken);

                   if(){

                   }
                   await Task.Delay(1000, stoppingToken);
               }
    }
}