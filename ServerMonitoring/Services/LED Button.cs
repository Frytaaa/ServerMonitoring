using System;
using Tinkerforge;

namespace ServerMonitoring.Services
{
    public class LED_Button
    {
        private static string HOST = "localhost";
        private static int PORT = 4223;
        private static string UID = "XBe";
        static void Main()
        {
            IPConnection ipcon = new IPConnection(); // Create IP connection
            BrickletRGBLEDButton rlb = new BrickletRGBLEDButton(UID, ipcon); // Create device object

            ipcon.Connect(HOST, PORT); // Connect to brickd
        }
    }
}
