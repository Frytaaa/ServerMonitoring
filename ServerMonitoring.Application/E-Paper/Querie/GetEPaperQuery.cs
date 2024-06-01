using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.E-Paper.Queries;

public class GetEPaperQuery
{
    public class GetEPaperQuery : IRequest<EPaperResponse>;
}