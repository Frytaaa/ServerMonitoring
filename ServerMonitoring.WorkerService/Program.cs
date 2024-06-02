using ServerMonitoring.Application;
using ServerMonitoring.Application.BrickletMotionDetectorV2;
using ServerMonitoring.Application.NFCScanner;
using ServerMonitoring.Infrastructure;
using ServerMonitoring.WorkerService.Extensions;
using ServerMonitoring.WorkerService.WorkerServices;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication().AddInfrastructure();
builder.Services.ConfigureMailService(builder.Configuration);
builder.Services.AddSingleton<NFCService>();
builder.Services.AddSingleton<MotionService>();
builder.Services.ConfigureDevices(builder.Configuration);
builder.Services.AddHostedService<TinkerforgeConnectionHostedService>();
builder.Services.AddHostedService<TemperatureWorkerService>();
builder.Services.AddHostedService<HumidityWorkerService>();
builder.Services.AddHostedService<AmbientLightWorkerService>();
builder.Services.AddHostedService<LCDDisplayWorkerService>();

try
{
    var host = builder.Build();
    await host.RunAsync();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}