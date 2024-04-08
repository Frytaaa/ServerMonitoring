using Microsoft.EntityFrameworkCore;
namespace ServerMonitoring.Infrastructure;

public class MonitoringContext(DbContextOptions options) : DbContext(options)
{
    //public DbSet<Domain.Entities> Monitorings { get; set; }
}



