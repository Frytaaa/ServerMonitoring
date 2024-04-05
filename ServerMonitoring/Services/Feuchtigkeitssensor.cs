using System;
using Tinkerforge;

namespace ServerMonitoring.Services
{
    public class Feuchtigkeitssensor
    {
        private static string HOST = "localhost";
        private static int PORT = 4223;
        private static string UID = "ViW";

        static void Main()
        {
            IPConnection ipcon = new IPConnection(); // Create IP connection
            BrickletHumidityV2 h = new BrickletHumidityV2(UID, ipcon); // Create device object

            ipcon.Connect(HOST, PORT); // Connect to brickd
        }
    }
}
