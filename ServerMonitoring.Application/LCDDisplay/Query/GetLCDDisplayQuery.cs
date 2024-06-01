using MediatR;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.LCDDisplay.Queries
{
    public class GetCurrentTimeQuery : IRequest<TimeResponse> { }
}
