using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.LCDDisplay.Queries;

namespace ServerMonitoring.WorkerService.WorkerServices
{
    public class LCDDisplayWorkerService : BackgroundService
    {
        private readonly ILogger<LCDDisplayWorkerService> _logger;
        private readonly ISender _mediator;

        public LCDDisplayWorkerService(
            ILogger<LCDDisplayWorkerService> logger,
            ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await _mediator.Send(new GetCurrentTimeQuery(), stoppingToken);
                _logger.LogInformation("Current time retrieved from LCDDisplay: {CurrentTime}", response.CurrentTime);

                await Task.Delay(5000, stoppingToken); // Beispiel: Aktualisierung alle 5 Sekunden
            }
        }
    }
}
