using MediatR;
using ServerMonitoring.Application.Responses;

namespace ServerMonitoring.Application.LEDButtonBricklet.Queries;

public class GetLEDButtonQuery : IRequest<LEDButtonResponse>;