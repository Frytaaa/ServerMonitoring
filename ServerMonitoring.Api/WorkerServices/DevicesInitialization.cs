using ServerMonitoring.Application.AmbientLightBricklet;
using ServerMonitoring.Application.BrickletHumidityV2;
using ServerMonitoring.Application.BrickletMotionDetectorV2;
using ServerMonitoring.Application.NfcScanner;
using Tinkerforge;

namespace ServerMonitoring.Api.WorkerServices;

// initialization of devices which are used only with callbacks
// because DI will not create an instance of them without injecting them somewhere
public class DevicesInitializatio(BrickletHumidityV2 humidity, BrickletAmbientLightV3 ambientLightV3, BrickletNFC nfc, BrickletMotionDetectorV2 motionDetector, BrickletEPaper296x128 ePaper, BrickletLCD128x64 lcd) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        ambientLightV3.SetIlluminanceCallbackConfiguration(10000, false, 'o', 2000, 8000);
        humidity.SetHumidityCallbackConfiguration(10000, false, 'x', 1000, 10000);
        
        ePaper.DrawText(16, 48, BrickletEPaper296x128.FONT_24X32, BrickletEPaper296x128.COLOR_RED,
            BrickletEPaper296x128.ORIENTATION_HORIZONTAL, "BPC");
        ePaper.Draw();
        humidity.HumidityCallback += HumidityCallback.GetHumidityCallback;
        ambientLightV3.IlluminanceCallback += IlluminanceCallback.GetIlluminanceCallback;
        nfc.ReaderStateChangedCallback += NfcService.ReaderStateChangedCB;
        nfc.SetMode(BrickletNFC.MODE_READER);
        motionDetector.MotionDetectedCallback += MotionService.MotionDetectedCB;

        var currentTime = DateTime.Now.ToString("HH:mm:ss");
        // Uhrzeit auf dem LCD-Display anzeigen
        lcd.WriteLine(0, 0, currentTime); 
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}