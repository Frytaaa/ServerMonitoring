using MediatR;

namespace ServerMonitoring.Application.LEDButtonBricklet.Commands;

public class SetLEDButtonColorCommand : IRequest
{
    public LEDColor Color { get; init; }
}

public enum LEDColor
{
    Green,
    Red,
    Blue
}