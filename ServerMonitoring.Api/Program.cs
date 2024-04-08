//using AutoMapper;
using ServerMonitoring.Application;
//using ServerMonitoring.Application.Abstractions;
using ServerMonitoring.Infrastructure;
//using ServerMonitoring.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication().AddInfrastructure();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var connectionString = builder.Configuration.GetConnectionString("Default");
//builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<MonitoringContext>(optionsAction => optionsAction.UseSqlite(connectionString));
//builder.Services.AddTransient<IMonitoringRepository, MonitoringRepository>();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Event.Api",
        Description = "API for EventPlanner",
    });
});

var app = builder.Build();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
}

app.UseHttpsRedirection();
app.Run();