using System;

namespace ServerMonitoring.Application.BrickletHumidityV2;

public static class HumidityCallback
{
    public static void GetHumidityCallback(Tinkerforge.BrickletHumidityV2 sender, int humidity)
    {
        Console.WriteLine("Humidity: " + humidity/100.0 + " %RH");
        Console.WriteLine("Recommended humidity for human comfort is 40 to 60 %RH.");
    }
}