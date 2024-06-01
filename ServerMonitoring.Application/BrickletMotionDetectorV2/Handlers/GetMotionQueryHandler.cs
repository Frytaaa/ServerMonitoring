using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.BrickletMotionDetectorV2.Queries;
using ServerMonitoring.Application.Responses;
using Tinkerforge;

namespace ServerMonitoring.Application.BrickletMotionDetectorV2.QueryHandlers
{
    public class GetMotionQueryHandler : IRequestHandler<GetMotionQuery, MotionResponse>
    {
        private readonly BrickletMotionDetector _device;

        public GetMotionQueryHandler(BrickletMotionDetector device)
        {
            _device = device;
        }

        public Task<MotionResponse> Handle(GetMotionQuery request, CancellationToken cancellationToken)
        {
            var motionDetected = _device.GetMotionDetected();
            return Task.FromResult(new MotionResponse { Motion = motionDetected });
        }
    }
}
