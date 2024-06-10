using ServerMonitoring.Application.AmbientLightBricklet;
using ServerMonitoring.Application.BrickletHumidityV2;
using ServerMonitoring.Application.BrickletMotionDetectorV2;
using ServerMonitoring.Application.NfcScanner;
using Tinkerforge;

namespace ServerMonitoring.WorkerService.Extensions;

public static class ConfigureDevicesExtension
{
    public static void ConfigureDevices(this IServiceCollection services, IConfiguration configuration)
    {
        var devices = configuration.GetSection("Devices");
        services.AddSingleton<BrickletPTCV2>(sp =>
            new BrickletPTCV2(devices.GetSection("PTC")["UID"], sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletPiezoSpeakerV2>(sp =>
            new BrickletPiezoSpeakerV2(devices.GetSection("PiezoSpeaker")["UID"],
                sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletHumidityV2>(sp =>
        {
            var humidity = new BrickletHumidityV2(devices.GetSection("Humidity")["UID"],
                sp.GetRequiredService<IPConnection>());
            return humidity;
        });
        services.AddSingleton<BrickletAmbientLightV3>(sp =>
        {
            var ambientLight = new BrickletAmbientLightV3(devices.GetSection("AmbientLight")["UID"],
                sp.GetRequiredService<IPConnection>());
            return ambientLight;
        });

        services.AddSingleton<BrickletSegmentDisplay4x7V2>(sp =>
            new BrickletSegmentDisplay4x7V2(devices.GetSection("SegmentDisplay")["UID"],
                sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletLCD128x64>(sp =>
            new BrickletLCD128x64(devices.GetSection("LCD")["UID"], sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletNFC>(sp =>
        {
            var nfc = new BrickletNFC(devices.GetSection("NFC")["UID"], sp.GetRequiredService<IPConnection>());
            return nfc;
        });

        services.AddSingleton<BrickletRGBLEDButton>(sp =>
            new BrickletRGBLEDButton(devices.GetSection("RGBLEDButton")["UID"],
                sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletEPaper296x128>(sp =>
            new BrickletEPaper296x128(devices.GetSection("EPaper")["UID"], sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletMotionDetectorV2>(sp =>
        {
            var motionDetector = new BrickletMotionDetectorV2(devices.GetSection("MotionDetector")["UID"],
                sp.GetRequiredService<IPConnection>());
            return motionDetector;
        });
    }
}