using ServerMonitoring.Application;
using Tinkerforge;

namespace ServerMonitoring.WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IPConnection _ipConnection;
    private static string HOST = "172.20.10.242";
    private static int PORT = 4223;
    private static string UID = "R7M"; // Change XXYYZZ to the UID of your Stepper Brick
    public Worker(ILogger<Worker> logger, IPConnection ipConnection)
    {
        _logger = logger;
        _ipConnection = ipConnection;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        var device = new BrickletPTCV2("Wcg", _ipConnection);
        _ipConnection.Connect(HOST, PORT);
        var temperatureRespone = HandleTemperatureAsync(device);
        if (temperatureRespone.Status == TemperatureStatus.High)
        {
            var speaker = new BrickletPiezoSpeakerV2("R7M", _ipConnection);
            speaker.SetAlarm(1000, 5000, 3, 2, 1, 2);
        }
        if (temperatureRespone.Status == TemperatureStatus.Low)
        {
            var speaker = new BrickletPiezoSpeakerV2("R7M", _ipConnection);
            speaker.SetAlarm(1000, 5000, 3, 2, 1, 2);
        }
        _ipConnection.Disconnect();
    }
    
    private TemperatureResponse HandleTemperatureAsync(BrickletPTCV2 device)
    {
        var temperature = device.GetTemperature();
        _logger.LogInformation("Temperature: {Temperature} \u00b0C", temperature / 100.0);

        var response = new TemperatureResponse(temperature);

        response.Status = temperature switch
        {
            <= 2000 => TemperatureStatus.Low,
            >= 2200 => TemperatureStatus.High,
            _ => response.Status
        };
        return response;
    }
}