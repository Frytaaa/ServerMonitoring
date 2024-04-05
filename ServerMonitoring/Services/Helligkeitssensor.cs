using System;
using Tinkerforge;

namespace ServerMonitoring.Services
{
    public class Helligkeitssensor
    {
        private static string HOST = "localhost";
        private static int PORT = 4223;
        private static string UID = "Pdw";

        static void Main()
        {
            IPConnection ipcon = new IPConnection(); // Create IP connection
            BrickletAmbientLightV3 al = new BrickletAmbientLightV3(UID, ipcon); // Create device object

            ipcon.Connect(HOST, PORT); // Connect to brickd
        }
    }
}
