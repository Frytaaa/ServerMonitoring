using MediatR;
using Microsoft.Extensions.Logging;
using ServerMonitoring.Application.BrickletMotionDetectorV2.Queries;
using ServerMonitoring.Application.EPaper;
using ServerMonitoring.Application.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerMonitoring.WorkerService.WorkerServices
{
    public class MotionWorkerService : BackgroundService
    {
        private readonly ILogger<MotionWorkerService> _logger;
        private readonly ISender _mediator;
        private readonly IEPaperService _ePaperService;

        public MotionWorkerService(ILogger<MotionWorkerService> logger, ISender mediator, IEPaperService ePaperService)
        {
            _logger = logger;
            _mediator = mediator;
            _ePaperService = ePaperService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var motionResponse = await _mediator.Send(new GetMotionQuery(), stoppingToken);

                if (motionResponse.Motion)
                {
                    // Bewegung erkannt, BCP auf dem E-Paper anzeigen
                    await _ePaperService.DisplayMessage("BCP");
                }

                // Wartezeit zwischen den Überprüfungen
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
