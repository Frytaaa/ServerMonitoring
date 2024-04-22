using MediatR;

namespace ServerMonitoring.WorkerService.WorkerServices;

public class TemplateWorkerService(ILogger<TemplateWorkerService> logger, IMediator mediator) : BackgroundService
{
    private readonly ILogger<TemplateWorkerService> _logger = logger;
    private readonly IMediator _mediator = mediator;
    

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // do something
            await Task.Delay(1000, stoppingToken);
        }
    }
}