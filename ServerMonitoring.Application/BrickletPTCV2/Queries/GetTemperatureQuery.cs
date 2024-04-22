using MediatR;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.BrickletPTCV2.Queries;

public class GetTemperatureQuery : IRequest<TemperatureResponse>
{
    public Tinkerforge.BrickletPTCV2 Device { get; set; }
}