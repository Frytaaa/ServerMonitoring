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
            new BrickletPiezoSpeakerV2(devices.GetSection("PiezoSpeaker")["UID"], sp.GetRequiredService<IPConnection>()));

        services.AddSingleton<BrickletHumidityV2>(sp =>
            new BrickletHumidityV2(devices.GetSection("Humidity")["UID"], sp.GetRequiredService<IPConnection>()));
        
        services.AddSingleton<BrickletAmbientLightV3>(sp =>
            new BrickletAmbientLightV3(devices.GetSection("AmbientLight")["UID"],
                sp.GetRequiredService<IPConnection>()));
        services.AddSingleton<BrickletNFC>(sp =>
            {
                var nfc = new BrickletNFC(devices.GetSection("NFC")["UID"], sp.GetRequiredService<IPConnection>());
                var nfcService = sp.GetRequiredService<NFCService>();
                nfc.ReaderStateChangedCallback += nfcService.ReaderStateChangedCB;
                nfc.SetMode(BrickletNFC.MODE_READER);
                return nfc;
            });
    }
}