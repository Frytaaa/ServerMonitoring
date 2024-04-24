using ServerMonitoring.Application;
using ServerMonitoring.Infrastructure;
using ServerMonitoring.WorkerService;
using ServerMonitoring.WorkerService.Extensions;
using ServerMonitoring.WorkerService.WorkerServices;
using Tinkerforge;

var builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddSingleton<IConfiguration>();
builder.Services.AddApplication().AddInfrastructure();
builder.Services.ConfigureDevices(builder.Configuration);
builder.Services.AddHostedService<TinkerforgeConnectionHostedService>();
builder.Services.AddHostedService<TemperatureWorkerService>();
builder.Services.AddHostedService<HumidityWorkerService>();
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