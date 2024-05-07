using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.LEDButtonBricklet.Queries;
using ServerMonitoring.Application.Responses;

namespace ServerMonittoring.Application.LEDButtonBricklet.Handlers
{
    public class GetLEDButtonQueryHandler(Tinkerforge.BrickletRGBLEDButton device) : IRequestHandler<GetLEDButtonQuery, LEDButtonResponse>
    {
        public Task<LEDButtonResponse> Handle(GetLEDButtonQuery request, CancellationToken cancellationToken)
        {
            var response = new LEDButtonResponse();

            switch (response.Status)
            {
                case LEDButtonStatus.Red:
                    device.SetColor(255, 0, 0);
                    break;
                case LEDButtonStatus.Green:
                    device.SetColor(0, 255, 0);
                    break;
                case LEDButtonStatus.Blue:
                    device.SetColor(0, 0, 255);
                    break;
            }

            return Task.FromResult(response);
        }
    }
}
