using MediatR;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.AmbientLightBricklet.Queries;

public class GetAmbientQuery : IRequest<AmbientLightResponse>;