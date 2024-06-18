using System;
using Tinkerforge;

namespace ServerMonitoring.Application.BrickletMotionDetectorV2;

public static class MotionService
{
    public static void MotionDetectedCB(Tinkerforge.BrickletMotionDetectorV2 sender)
    {
        Console.WriteLine("Motion detected");
        sender.SetIndicator(255, 123, 200);
    }
    public static void DetectionCycleEndedCB(Tinkerforge.BrickletMotionDetectorV2 sender)
    {
        Console.WriteLine("Detection Cycle Ended (next detection possible in ~2 seconds)");
    }
}