using MediatR;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.BrickletHumidityV2.Queries;

public class GetHumidityQuery: IRequest<HumidityResponse>
{
    public Tinkerforge.BrickletHumidityV2 Device { get; set; }
}