using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.BrickletHumidityV2.Queries;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.BrickletHumidityV2.Handlers;

public class GetHumidityQueryHandler(Tinkerforge.BrickletHumidityV2 device)
    : IRequestHandler<GetHumidityQuery, HumidityResponse>
{
    public Task<HumidityResponse> Handle(GetHumidityQuery request, CancellationToken cancellationToken)
    {
        var humidity = device.GetHumidity();
        var response = new HumidityResponse
        {
            Humidity = humidity / 100.0,
            Status = humidity switch
            {
                < 4000 => HumidityStatus.Low,
                >= 6000 => HumidityStatus.High,
                _ => HumidityStatus.Normal
            }
        };

        return Task.FromResult(response);
    }
}