using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.SegmentDisplay.Commands;
using Tinkerforge;

namespace ServerMonitoring.Application.SegmentDisplay.CommandHandlers;

public class SetSegmentsCommandHandler(BrickletSegmentDisplay4x7V2 device) : IRequestHandler<SetSegmentsCommand>
{
    public Task Handle(SetSegmentsCommand request, CancellationToken cancellationToken)
    {
        // Convert temperature to a string with one decimal place
        var temperatureStr = request.Temperature.ToString("F1");

        // Ensure the string fits the 4x7 display
        if (temperatureStr.Length > 4)
        {
            temperatureStr = temperatureStr.Substring(0, 4);
        }

        // Create segment arrays for each digit
        var digit0 = new bool[8];
        var digit1 = new bool[8];
        var digit2 = new bool[8];
        var digit3 = new bool[8];

        // Map characters to their corresponding segments
        var charToSegments = new Dictionary<char, bool[]>
        {
            { '0', [true, true, true, true, true, true, false, false] },
            { '1', [false, true, true, false, false, false, false, false] },
            { '2', [true, true, false, true, true, false, true, false] },
            { '3', [true, true, true, true, false, false, true, false] },
            { '4', [false, true, true, false, false, true, true, false] },
            { '5', [true, false, true, true, false, true, true, false] },
            { '6', [true, false, true, true, true, true, true, false] },
            { '7', [true, true, true, false, false, false, false, false] },
            { '8', [true, true, true, true, true, true, true, false] },
            { '9', [true, true, true, true, false, true, true, false] },
            { '.', [false, false, false, false, false, false, true, false] }
        };

        // Assign segments to digits based on the temperatureStr
        var segments = new[] { digit0, digit1, digit2, digit3 };
        for (var i = 0; i < temperatureStr.Length; i++)
        {
            var c = temperatureStr[i];
            if (charToSegments.TryGetValue(c, out var segment))
            {
                segments[i] = segment;
            }
        }

        // Assuming colon and tick are not used in this context
        var colon = new[] { false, false };
        const bool tick = false;

        // Display the temperature on the Segment Display
        device.SetSegments(digit0, digit1, digit2, digit3, colon, tick);
        return Task.CompletedTask;
    }
}