using Tinkerforge;

namespace ServerMonitoring.WorkerService.WorkerServices;

// initialization of devices which are used only with callbacks
// because DI will not create an instance of them without injecting them somewhere
public class DevicesInitialization(BrickletPTCV2 temperatureSensor, BrickletHumidityV2 humidity, BrickletAmbientLightV3 ambientLightV3, BrickletNFC nfc, BrickletMotionDetectorV2 motionDetector) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        // TODO check if the devices have to be used there or is injection enough

        ambientLightV3.SetIlluminanceCallbackConfiguration(10000, false, 'o', 2000, 8000);
        temperatureSensor.SetTemperatureCallbackConfiguration(10000, false, 'o', 1700, 3500);
        humidity.SetHumidityCallbackConfiguration(10000, false, 'x', 1000, 10000);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}