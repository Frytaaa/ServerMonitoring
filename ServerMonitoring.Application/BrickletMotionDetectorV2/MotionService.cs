using System;
using Tinkerforge;

namespace ServerMonitoring.Application.BrickletMotionDetectorV2;

public class MotionService(BrickletEPaper296x128 ePaper)
{
    public void MotionDetectedCB(Tinkerforge.BrickletMotionDetectorV2 sender)
    {
        ePaper.DrawText(16, 48, BrickletEPaper296x128.FONT_24X32, BrickletEPaper296x128.COLOR_RED,
            BrickletEPaper296x128.ORIENTATION_HORIZONTAL, "BPC");
        ePaper.Draw();
    }
    public void DetectionCycleEndedCB(Tinkerforge.BrickletMotionDetectorV2 sender)
    {
        // TODO clear display
        Console.WriteLine("Detection Cycle Ended (next detection possible in ~2 seconds)");
    }
}