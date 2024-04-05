using System;
using Tinkerforge;

namespace ServerMonitoring.Services
{
    public class NFC_Scanner
    {
        private static string HOST = "localhost";
        private static int PORT = 4223;
        private static string UID = "22ND";

        static void Main()
        {
            IPConnection ipcon = new IPConnection(); // Create IP connection
            BrickletNFC nfc = new BrickletNFC(UID, ipcon); // Create device object

            ipcon.Connect(HOST, PORT); // Connect to brickd
        }
    }
}
