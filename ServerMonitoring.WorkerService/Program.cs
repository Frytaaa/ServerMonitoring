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

builder.Services.AddHostedService<TemperatureWorkerService>();
builder.Services.AddHostedService<HumidityWorkerService>();

var ipConnection = builder.Services.BuildServiceProvider().GetService<IPConnection>();
ipConnection.Connect(builder.Configuration.GetSection("Tinkerforge")["Host"],
    int.Parse(builder.Configuration.GetSection("Tinkerforge")["Port"]));
try
{
    var host = builder.Build();
    host.Run();
}
catch (Exception e)
{
    ipConnection.Disconnect();
    Console.WriteLine(e);
    throw;
}