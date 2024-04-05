using System;
using Tinkerforge;

namespace ServerMonitoring.Services
{
    public class LCD_Display
    {
        private static string HOST = "localhost";
        private static int PORT = 4223;
        private static string UID = "24Rh";

        static void Main()
        {
            IPConnection ipcon = new IPConnection(); // Create IP connection
            BrickletLCD128x64 lcd = new BrickletLCD128x64(UID, ipcon); // Create device object

            ipcon.Connect(HOST, PORT); // Connect to brickd
        }
    }
}
