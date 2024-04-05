using System;
using Tinkerforge;

namespace ServerMonitoring.Services
{
    public class DualButtonBricklet
    {
        private static string HOST = "localhost";
        private static int PORT = 4223;
        private static string UID = "23Qx"; // Change XYZ to the UID of your Dual Button Bricklet 2.0

        static void Main()
        {
            IPConnection ipcon = new IPConnection(); // Create IP connection
            BrickletDualButtonV2 db = new BrickletDualButtonV2(UID, ipcon); // Create device object

            ipcon.Connect(HOST, PORT); // Connect to brickd

        }
    }
}
