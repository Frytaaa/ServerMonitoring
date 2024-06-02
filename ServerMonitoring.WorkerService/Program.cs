using ServerMonitoring.Application;
using ServerMonitoring.Application.NFCScanner;
using ServerMonitoring.Infrastructure;
using ServerMonitoring.WorkerService.Extensions;
using ServerMonitoring.WorkerService.WorkerServices;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication().AddInfrastructure();
builder.Services.ConfigureMailService(builder.Configuration);
builder.Services.AddSingleton<NFCService>();
builder.Services.ConfigureDevices(builder.Configuration);
builder.Services.AddHostedService<TinkerforgeConnectionHostedService>();
builder.Services.AddHostedService<TemperatureWorkerService>();
builder.Services.AddHostedService<HumidityWorkerService>();
builder.Services.AddHostedService<AmbientLightWorkerService>();

try
{
    var host = builder.Build();
    host.Run();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}