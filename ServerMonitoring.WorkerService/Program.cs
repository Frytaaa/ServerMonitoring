using ServerMonitoring.Application;

using ServerMonitoring.Infrastructure;
using ServerMonitoring.WorkerService.Extensions;
using ServerMonitoring.WorkerService.WorkerServices;
using Tinkerforge;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication().AddInfrastructure();
builder.Services.ConfigureMailService(builder.Configuration);
builder.Services.AddSingleton<IPConnection>();
builder.Services.AddHostedService<TinkerforgeConnectionHostedService>();
builder.Services.ConfigureDevices(builder.Configuration);
builder.Services.AddHostedService<TemperatureWorkerService>();
builder.Services.AddHostedService<LcdDisplayWorkerService>();

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