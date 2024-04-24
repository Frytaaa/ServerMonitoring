using MediatR;
using ServerMonitoring.Application.BrickletMotionDetectorV2.Queries;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.WorkerService.WorkerServices;

public class TemplateWorkerService(ILogger<MotionWorkerService> logger, IMediator mediator
BrickletMotionDetectorV2 motiondetector) : BackgroundService
{
    //private readonly ILogger<MotionWorkerService> _logger = logger;
    //private readonly IMediator _mediator = mediator;


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // do something
            await Task.Delay(1000, stoppingToken);
        }
    }
}