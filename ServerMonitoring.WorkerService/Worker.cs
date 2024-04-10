using MediatR;
using ServerMonitoring.Application;
using ServerMonitoring.Application.BrickletPTCV2.Queries;
using Tinkerforge;

namespace ServerMonitoring.WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IPConnection _ipConnection;
    private readonly BrickletPTCV2 _brickletPtcv2;
    private static string HOST = "172.20.10.242";
    private static int PORT = 4223;
    private static string UID = "R7M"; // Change XXYYZZ to the UID of your Stepper Brick
    private IMediator _mediator;
    
    public Worker(ILogger<Worker> logger, IPConnection ipConnection, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
        _ipConnection = ipConnection;
        _ipConnection.Connect(HOST, PORT);
        _brickletPtcv2 = new BrickletPTCV2("Wcg", _ipConnection);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var temperatureResponse = await _mediator.Send(new GetTemperatureQuery { Device = _brickletPtcv2 }, stoppingToken);
        
        if (temperatureResponse.Status == TemperatureStatus.High)
        {
            var speaker = new BrickletPiezoSpeakerV2("R7M", _ipConnection);
            speaker.SetAlarm(1000, 5000, 3, 2, 1, 2);
        }

        _ipConnection.Disconnect();
    }
}