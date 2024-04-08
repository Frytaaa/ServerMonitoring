using ServerMonitoring.WorkerService;
using Tinkerforge;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddTransient<IPConnection>(sp => new IPConnection());
var host = builder.Build();
host.Run();