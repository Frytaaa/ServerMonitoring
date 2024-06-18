using ServerMonitoring.Api.Extensions;
using ServerMonitoring.Api.Hubs;
using ServerMonitoring.Api.WorkerServices;
using ServerMonitoring.Application;
using Tinkerforge;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.ConfigureMailService(builder.Configuration);
builder.Services.AddSingleton<IPConnection>();
builder.Services.AddHostedService<TinkerforgeConnectionHostedService>();
builder.Services.ConfigureDevices(builder.Configuration);
builder.Services.AddHostedService<SignalRWorkerService>();
builder.Services.AddHostedService<TemperatureWorkerService>();
builder.Services.AddHostedService<LcdDisplayWorkerService>();
builder.Services.AddHostedService<DevicesInitializatio>();

var app = builder.Build();
app.MapHub<MonitoringHub>("/hub");
app.UseCors("AllowAllOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


await app.RunAsync();