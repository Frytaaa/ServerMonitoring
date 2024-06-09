using System;
using Tinkerforge;

namespace ServerMonitoring.Application.AmbientLightBricklet;

public static class IlluminanceCallback
{
    public static void GetIlluminanceCallback(BrickletAmbientLightV3 sender, long illuminance)
    {
        switch (illuminance)
        {
            case < 2000:
                Console.WriteLine($"Ambient light is low {illuminance}");
                break;
            case >= 8000:
                Console.WriteLine($"Ambient light is high {illuminance}");
                break;
            default:
                Console.WriteLine("Ambient light is normal");
                break;
        }
    }
}