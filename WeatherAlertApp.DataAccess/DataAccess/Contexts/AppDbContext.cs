using WeatherAlertApp.Common.Entities;

namespace WeatherAlertApp.DataAccess.DataAccess.Contexts;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<WeatherForecastEntity> WeatherForecasts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
