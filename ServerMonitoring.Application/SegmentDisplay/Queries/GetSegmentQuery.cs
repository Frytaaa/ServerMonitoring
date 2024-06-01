using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.SegmentDisplay.Queries;

public class GetSegmentQuery
{
    public class GetSegmentQuery : IRequest<SegmentResponse>;
}
