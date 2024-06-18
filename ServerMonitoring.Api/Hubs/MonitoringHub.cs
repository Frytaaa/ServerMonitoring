using Microsoft.AspNetCore.SignalR;
using Tinkerforge;

namespace ServerMonitoring.Api.Hubs;

public class MonitoringHub : Hub
{
    public async Task ChangeNfcMode(int mode)
    {
        var newMode = mode switch
        {
            0 => BrickletNFC.MODE_READER,
            3 => BrickletNFC.MODE_OFF,
            _ => new byte()
        };
        await Clients.All.SendAsync("NFCModeChanged", newMode);
    }
}