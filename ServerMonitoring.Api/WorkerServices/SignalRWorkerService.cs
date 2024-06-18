using Microsoft.AspNetCore.SignalR;
using ServerMonitoring.Api.Hubs;
using Tinkerforge;

namespace ServerMonitoring.Api.WorkerServices;

public class SignalRWorkerService(IHubContext<MonitoringHub> hubContext, BrickletPTCV2 brickletPtcv2, BrickletHumidityV2 humidityV2, BrickletNFC nfcBricklet) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await SendUpdatesByHub();
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task SendUpdatesByHub()
    {
        var temperature = brickletPtcv2.GetTemperature();
        var humidity = humidityV2.GetHumidity();
        nfcBricklet.ReaderGetTagID(out var tagType, out var tagId);
        
        await hubContext.Clients.All.SendAsync("UpdateTemperature",  temperature / 100.0);
        await hubContext.Clients.All.SendAsync("UpdateHumidity", humidity / 100.0);
        await hubContext.Clients.All.SendAsync("SendNfcTag", tagId, tagType);
        await Task.CompletedTask;
    }
}