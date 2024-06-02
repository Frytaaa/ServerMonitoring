using ServerMonitoring.Application.BrickletMotionDetectorV2;
using ServerMonitoring.Application.NFCScanner;
using Tinkerforge;

namespace ServerMonitoring.WorkerService.Extensions;

public static class ConfigureDevicesExtension
{
    public static void ConfigureDevices(this IServiceCollection services, IConfiguration configuration)
    {
        var devices = configuration.GetSection("Devices");

        services.AddSingleton<IPConnection>();
        services.AddSingleton<BrickletPTCV2>(sp =>
            new BrickletPTCV2(devices.GetSection("PTC")["UID"], sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletPiezoSpeakerV2>(sp =>
            new BrickletPiezoSpeakerV2(devices.GetSection("PiezoSpeaker")["UID"],
                sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletHumidityV2>(sp =>
            new BrickletHumidityV2(devices.GetSection("Humidity")["UID"], sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletAmbientLightV3>(sp =>
            new BrickletAmbientLightV3(devices.GetSection("AmbientLight")["UID"],
                sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletSegmentDisplay4x7V2>(sp =>
            new BrickletSegmentDisplay4x7V2(devices.GetSection("SegmentDisplay")["UID"],
                sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletLCD128x64>(sp =>
            new BrickletLCD128x64(devices.GetSection("LCD")["UID"], sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletNFC>(sp =>
        {
            var nfc = new BrickletNFC(devices.GetSection("NFC")["UID"], sp.GetRequiredService<IPConnection>());
            var nfcService = sp.GetRequiredService<NFCService>();
            nfc.ReaderStateChangedCallback += nfcService.ReaderStateChangedCB;
            nfc.SetMode(BrickletNFC.MODE_READER);
            return nfc;
        });

        services.AddSingleton<BrickletEPaper296x128>(sp =>
            new BrickletEPaper296x128(devices.GetSection("EPaper")["UID"], sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletMotionDetectorV2>(sp =>
        {
            var motionDetector = new BrickletMotionDetectorV2(devices.GetSection("MotionDetector")["UID"],
                sp.GetRequiredService<IPConnection>());
            var motionService = sp.GetRequiredService<MotionService>();

            motionDetector.MotionDetectedCallback += motionService.MotionDetectedCB;
            motionDetector.DetectionCycleEndedCallback += motionService.DetectionCycleEndedCB;
            return motionDetector;
        });
    }
}