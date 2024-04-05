using System;
using Tinkerforge;

namespace ServerMonitoring.Services
{
    public class Bewegungsmelder
    {
        private static string HOST = "localhost";
        private static int PORT = 4223;
        private static string UID = "ML4"; // Change XYZ to the UID of your Motion Detector Bricklet 2.0

        static void Main()
        {
            IPConnection ipcon = new IPConnection(); // Create IP connection
            BrickletMotionDetectorV2 md =
              new BrickletMotionDetectorV2(UID, ipcon); // Create device object

            ipcon.Connect(HOST, PORT); // Connect to brickd


            // Turn blue backlight LEDs on (maximum brightness)
            md.SetIndicator(255, 255, 255);
        }
    }
}
