using MediatR;

namespace ServerMonitoring.Application.SegmentDisplay.Commands;

public class SetSegmentsCommand : IRequest
{
    public double Temperature { get; init; }
}