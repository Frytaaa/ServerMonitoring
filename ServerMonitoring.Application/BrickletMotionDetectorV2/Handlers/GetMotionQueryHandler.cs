using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.BrickletMotionDetectorV2.Queries;
using ServerMonitoring.Application.Responses;
using Tinkerforge;

namespace ServerMonitoring.Application.BrickletMotionDetectorV2.QueryHandlers;

public class GetMotionQueryHandlers(Tinkerforge.BrickletMotionDetector device)
: IRequestHandler<GetMotionQuery, MotionResponse>
{
     public Task<MotionResponse> Handle(GetMotionQuery request, CancellationToken cancellationToken)
     {

     }
}