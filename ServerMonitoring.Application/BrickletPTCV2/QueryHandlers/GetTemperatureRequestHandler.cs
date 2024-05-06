using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.BrickletPTCV2.Queries;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.BrickletPTCV2.QueryHandlers;

public class GetTemperatureRequestHandler(Tinkerforge.BrickletPTCV2 device)
    : IRequestHandler<GetTemperatureQuery, TemperatureResponse>
{
    public Task<TemperatureResponse> Handle(GetTemperatureQuery request, CancellationToken cancellationToken)
    {
        var temperature = device.GetTemperature();
        var response = new TemperatureResponse
        {
            Temperature = temperature / 100.0,
            Status = temperature switch
            {
                <= 1700 => TemperatureStatus.Low,
                (> 2700) and (< 3500) => TemperatureStatus.High,
                >= 3500 => TemperatureStatus.Critical,
                _ => TemperatureStatus.Normal
            }
        };

        return Task.FromResult(response);
    }
}