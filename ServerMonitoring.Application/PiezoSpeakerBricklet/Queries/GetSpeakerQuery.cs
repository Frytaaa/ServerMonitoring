using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.PiezoSpeakerBricklet.Queries;

public class GetSpeakerQuery
{
    public class GetAmbientQuery : IRequest<PiezoSpeakerResponse>;
}