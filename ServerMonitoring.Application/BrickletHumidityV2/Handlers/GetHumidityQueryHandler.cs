using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.BrickletHumidityV2.Queries;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.BrickletHumidityV2.Handlers;

public class GetHumidityQueryHandler : IRequestHandler<GetHumidityQuery, HumidityResponse>
{
    public Task<HumidityResponse> Handle(GetHumidityQuery request, CancellationToken cancellationToken)
    {
        var humidity = request.Device.GetHumidity();
        var response = new HumidityResponse(humidity)
        {
            Status = humidity switch
            {
                <= 30 => HumidityStatus.Low,
                >= 55 => HumidityStatus.High,
                _ => HumidityStatus.Normal
            }
        };

        return Task.FromResult(response);
    }
}