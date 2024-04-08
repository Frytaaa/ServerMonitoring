using System;
using Tinkerforge;

namespace ServerMonitoring.Services
{
    public class Piezo_Speaker
    {
        private static string Host = "172.20.10.242";
        private static int Port = 4223;
        private static string UID = "R7M"; // Change XYZ to the UID of your Piezo Speaker Bricklet 2.0
        private static int StartFrequency = 800;
        private static int EndFrequency = 2000;
        private static int StepSize = 10;
        private static int StepDelay = 1;
        private static byte Volume = 2;
        private static long Duration = 10000;

        public void Alarm()
        {

            IPConnection ipcon = new IPConnection(); // Create IP connection
            BrickletPiezoSpeakerV2 ps = new BrickletPiezoSpeakerV2(UID, ipcon); // Create device object

            ipcon.Connect(Host, Port); // Connect to brickd

            // 2 seconds of annoying fast alarm
            ps.SetAlarm(StartFrequency, EndFrequency, StepSize, StepDelay, Volume, Duration);

        }
    }
}
