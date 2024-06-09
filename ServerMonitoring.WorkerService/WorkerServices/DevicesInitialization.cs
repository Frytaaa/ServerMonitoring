using Tinkerforge;

namespace ServerMonitoring.WorkerService.WorkerServices;

// initialization of devices which are used only with callbacks
// because DI will not create an instance of them without injecting them somewhere
public class DevicesInitialization(BrickletPTCV2 temperatureSensor, BrickletHumidityV2 humidity, BrickletAmbientLightV3 ambientLightV3, BrickletNFC nfc, BrickletMotionDetectorV2 motionDetector) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // TODO check if the devices have to be used there or is injection enough
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}