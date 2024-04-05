using System;
using Tinkerforge;

namespace ServerMonitoring.Services
{
    public class E_Paper_Display
    {
        private static string HOST = "localhost";
        private static int PORT = 4223;
        private static string UID = "XGL";

        static void Main()
        {
            IPConnection ipcon = new IPConnection(); // Create IP connection
            BrickletEPaper296x128 ep = new BrickletEPaper296x128(UID, ipcon); // Create device object

            ipcon.Connect(HOST, PORT); // Connect to brickd

        }
    }
}
