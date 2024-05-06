using MediatR;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.BrickletMotionDetectorV2.Queries;

public class GetMotionQuery : IRequest<MotionResponse>;