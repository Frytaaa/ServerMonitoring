using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.LCDDisplay.Queries;
using ServerMonitoring.Application.Responses;
using Tinkerforge;

namespace ServerMonitoring.Application.LCDDisplay.GetLCDDisplayQueryHandlers
{
    public class GetCurrentTimeRequestHandler : IRequestHandler<GetCurrentTimeQuery, TimeResponse>
    {
        private readonly BrickletLCD128x64 _lcdDisplay;

        public GetCurrentTimeRequestHandler(BrickletLCD128x64 lcdDisplay)
        {
            _lcdDisplay = lcdDisplay;
        }

        public Task<TimeResponse> Handle(GetCurrentTimeQuery request, CancellationToken cancellationToken)
        {
            // Aktuelle Uhrzeit abrufen
            var currentTime = DateTime.Now.ToString("HH:mm:ss");

            // Uhrzeit auf dem LCD-Display anzeigen
            _lcdDisplay.WriteLines(0, 0, 0, currentTime); // Beispiel: Zeile 0, Spalte 0, Inhalt: currentTime

            // Rückgabe der aktuellen Uhrzeit
            return Task.FromResult(new TimeResponse { CurrentTime = currentTime });
        }
    }
}
