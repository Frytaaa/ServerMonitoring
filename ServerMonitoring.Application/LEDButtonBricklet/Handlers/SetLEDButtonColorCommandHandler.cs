using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.LEDButtonBricklet.Commands;

namespace ServerMonitoring.Application.LEDButtonBricklet.Handlers
{
    public class SetLEDButtonColorCommandHandler(Tinkerforge.BrickletRGBLEDButton device) : IRequestHandler<SetLEDButtonColorCommand>
    {
        public Task Handle(SetLEDButtonColorCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("SetLEDButtonColorCommandHandler");
            Console.WriteLine(request.Color);
            switch (request.Color)
            {
                case LEDColor.Red:
                    device.SetColor(255, 0, 0);
                    break;
                case LEDColor.Green:
                    device.SetColor(0, 255, 0);
                    break;
                case LEDColor.Blue:
                    device.SetColor(0, 0, 255);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Task.CompletedTask;
        }
    }
}
