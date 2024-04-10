using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.BrickletPTCV2.Queries;

namespace ServerMonitoring.Application.BrickletPTCV2.QueryHandlers;

public class GetTemperatureRequestHandler : IRequestHandler<GetTemperatureQuery, TemperatureResponse>
{
    public Task<TemperatureResponse> Handle(GetTemperatureQuery request, CancellationToken cancellationToken)
    {
        var temperature = request.Device.GetTemperature();
        var response = new TemperatureResponse(temperature);

        response.Status = temperature switch
        {
            <= 2000 => TemperatureStatus.Low,
            >= 2200 => TemperatureStatus.High,
            _ => response.Status
        };
        return Task.FromResult(response);
    }
}