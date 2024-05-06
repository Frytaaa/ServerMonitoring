using MediatR;
using ServerMonitoring.Application;
using ServerMonitoring.Application.BrickletMotionDetectorV2.Queries;
using ServerMonitoring.Application.Responses;
using Tinkerforge;

public class MotionWorkerService(
ILogger<MotionWorkerService> logger,
ISender mediator,
BrickletPiezoSpeakerV2 brickletPiezoSpeakerV2,
BrickletMotionDetector md,
MailService mailService)
{
    md.SetMotionDetectionPeriod(100);
    // Callback function for motion detected callback
    static void MotionDetectedCB(BrickletMotionDetector sender)
    {
        Console.WriteLine("Motion Detected");
        logger.LogCritical("Motion detected ${sender}", response.Temperature);
        mailService.SendMail("Motion detected",
                                    $"Motion has been detected");
        speaker.SetAlarm(1000, 5000, 3, 2, 1, 2);
    }

    // Callback function for detection cycle ended callback
    static void DetectionCycleEndedCB(BrickletMotionDetector sender)
    {
        Console.WriteLine("Detection Cycle Ended (next detection possible in ~3 seconds)");
    }

    static void Main()
    {
       // Register motion detected callback to function MotionDetectedCB
        md.MotionDetectedCallback += MotionDetectedCB;

        // Register detection cycle ended callback to function DetectionCycleEndedCB
        md.DetectionCycleEndedCallback += DetectionCycleEndedCB;
    }
}