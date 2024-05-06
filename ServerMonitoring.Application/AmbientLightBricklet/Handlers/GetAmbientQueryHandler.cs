using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.AmbientLightBricklet.Queries;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.AmbientLightBricklet.Handlers
{
    public class GetAmbientQueryHandler(Tinkerforge.BrickletAmbientLightV3 device) : IRequestHandler<GetAmbientQuery, AmbientLightResponse>
    {
        public Task<AmbientLightResponse> Handle(GetAmbientQuery request, CancellationToken cancellationToken)
        {
            var ambientLight = device.GetIlluminance();
            var response = new AmbientLightResponse
            {
                AmbientLight = ambientLight,
                Status = ambientLight switch
                {
                    < 2000 => AmbientLightStatus.Low,
                    >= 8000 => AmbientLightStatus.High,
                    _ => AmbientLightStatus.Normal
                }
            };

            return Task.FromResult(response);
        }
    }
}