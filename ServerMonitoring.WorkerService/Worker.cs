using Tinkerforge;

namespace ServerMonitoring.WorkerService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private static string HOST = "172.20.10.242";
    private static int PORT = 4223;
    private static string UID = "R7M"; // Change XXYYZZ to the UID of your Stepper Brick
    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var ipConnection = new IPConnection(); // Create IP connection
        
        // var ps = new BrickletPiezoSpeakerV2(UID, ipConnection); // Create device object
        //
        // ipConnection.Connect(HOST, PORT); // Connect to brickd
        // // Don't use device before ipcon is connected
        //
        // // 10 seconds of loud annoying fast alarm
        // ps.SetAlarm(800, 2000, 10, 1, 1, 10000);
        //
        // Console.WriteLine("Press enter to exit");
        // Console.ReadLine();
        // ipConnection.Disconnect();
    }
}