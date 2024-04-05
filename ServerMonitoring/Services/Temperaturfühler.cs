using System;
using Tinkerforge;

namespace ServerMonitoring.Services
{
    public class Temperaturfühler
    {
        private static string HOST = "localhost";
        private static int PORT = 4223;
        private static string UID = "Wcg";

        static void Main()
        {
            IPConnection ipcon = new IPConnection(); // Create IP connection
            BrickletPTCV2 ptc = new BrickletPTCV2(UID, ipcon); // Create device object

            ipcon.Connect(HOST, PORT); // Connect to brickd


            // Get current temperature
            int temperature = ptc.GetTemperature();
            Console.WriteLine("Temperature: " + temperature / 100.0 + " °C");
        }
    }
}
