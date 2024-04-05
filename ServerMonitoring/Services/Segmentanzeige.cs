using System;
using Tinkerforge;

namespace ServerMonitoring.Services
{
    public class Segmentanzeige
    {
        private static string HOST = "localhost";
        private static int PORT = 4223;
        private static string UID = "Tre";
        static void Main()
        {
            IPConnection ipcon = new IPConnection(); // Create IP connection
            BrickletSegmentDisplay4x7V2 sd =
              new BrickletSegmentDisplay4x7V2(UID, ipcon); // Create device object

            ipcon.Connect(HOST, PORT); // Connect to brickd
        }
    }
}
