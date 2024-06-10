using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ServerMonitoring.Application.LCDDisplay.Commands;
using Tinkerforge;

namespace ServerMonitoring.Application.LCDDisplay.CommandHandlers
{
    public class ShowCurrentTimeCommandHandler(BrickletLCD128x64 device) : IRequestHandler<ShowCurrentTimeCommand>
    {
        public Task Handle(ShowCurrentTimeCommand request, CancellationToken cancellationToken)
        {
  
            // Aktuelle Uhrzeit abrufen
            var currentTime = DateTime.Now.ToString("HH:mm:ss");

            // Uhrzeit auf dem LCD-Display anzeigen
            device.WriteLine(0, 0, currentTime); // Beispiel: Zeile 0, Spalte 0, Inhalt: currentTime

            return Task.CompletedTask;
        }
    }
}