using System.Net;
using System.Net.Mail;
using ServerMonitoring.Application;
using ServerMonitoring.Infrastructure;
using ServerMonitoring.WorkerService.Extensions;
using ServerMonitoring.WorkerService.WorkerServices;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication().AddInfrastructure();
// TODO extract mail configuration to a separate class
builder.Services.AddSingleton<SmtpClient>(_ =>
{
    var smtpClient = new SmtpClient();
    smtpClient.Host = builder.Configuration["MailConfiguration:SmtpServer"];
    smtpClient.Port = int.Parse(builder.Configuration["MailConfiguration:SmtpPort"]);
    smtpClient.Credentials = new NetworkCredential(builder.Configuration["MailConfiguration:SmtpUser"],
        builder.Configuration["MailConfiguration:SmtpPassword"]);
    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
    smtpClient.EnableSsl = true;
    return smtpClient;
});
builder.Services.AddTransient<MailService>();
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