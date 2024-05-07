using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.DualButtonBrickletV2.Queries;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.DualButtonBrickletV2.Handlers
{
    public class GetDualButtonQueryHandler(Tinkerforge.BrickletDualButtonV2 device) : IRequestHandler<GetDualButtonQuery, DualButtonResponses>
    {
        public Task<DualButtonResponses> Handle(GetDualButtonQuery request, CancellationToken cancellationToken)
        {
            var response = new DualButtonResponses();

            device.GetButtonState(out byte buttonL, out byte buttonR);
            {
                if (buttonL == Tinkerforge.BrickletDualButtonV2.BUTTON_STATE_PRESSED)
                {
                    response.Status = DualButtonStatus.Left;
                }
                else if (buttonL == Tinkerforge.BrickletDualButtonV2.BUTTON_STATE_RELEASED)
                {
                    response.Status = DualButtonStatus.None;
                }
                if (buttonR == Tinkerforge.BrickletDualButtonV2.BUTTON_STATE_PRESSED)
                {
                    response.Status = DualButtonStatus.Right;
                }
                else if (buttonR == Tinkerforge.BrickletDualButtonV2.BUTTON_STATE_RELEASED)
                {
                    response.Status = DualButtonStatus.None;
                }
            };

            return Task.FromResult(response);
        }
    }
}
