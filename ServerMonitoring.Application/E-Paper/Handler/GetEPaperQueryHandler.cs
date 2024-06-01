using MediatR;
using ServerMonitoring.Application.EPaper.Queries;
using ServerMonitoring.Application.Responses;
using ServerMonitoring.Application.EPaper;
using System.Threading;
using System.Threading.Tasks;

namespace ServerMonitoring.Application.EPaper.Handlers
{
    public class GetEPaperQueryHandler : IRequestHandler<GetEPaperQuery, EPaperResponse>
    {
        private readonly IEPaperService _ePaperService;

        public GetEPaperQueryHandler(IEPaperService ePaperService)
        {
            _ePaperService = ePaperService;
        }

        public async Task<EPaperResponse> Handle(GetEPaperQuery request, CancellationToken cancellationToken)
        {
            // Hier könnte man zusätzliche Logik implementieren, um die Antwort zu generieren
            var response = new EPaperResponse();

            // Nachricht auf dem E-Paper anzeigen
            await _ePaperService.DisplayMessage("BCP");

            // Rückgabe der Antwort
            return response;
        }
    }
}
