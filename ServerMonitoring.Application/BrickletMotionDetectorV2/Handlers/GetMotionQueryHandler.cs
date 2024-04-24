using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.BrickletMotionDetectorV2.Queries;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.BrickletMotionDetectorV2.QueryHandlers;

public class GetMotionRequestHandler : IRequestHandler<GetMotionQuery, MotionResponse>
{
    public Task<MotionResponse> Handle(GetMotionQuery request, CancellationToken cancellationToken)
    {
        var motion = request.Device.GetMotion();
        var response = new MotionResponse(motion)
        {
            Status = motion switch
            {
                <= 2200 => MotionStatus.Active,
                _ => MotionStatus.NotActive,
            }
        };

        return Task.FromResult(response);
    }
}