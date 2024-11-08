using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WeatherAlertApp.Common.Entities.Configurations;

public class WeatherForecastEntityConfiguration : IEntityTypeConfiguration<WeatherForecastEntity>
{
    public void Configure(EntityTypeBuilder<WeatherForecastEntity> builder)
    {
        builder.HasKey(wf => wf.Id);

        builder.Property(wf => wf.City)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(wf => wf.Date)
            .IsRequired();

        builder.Property(wf => wf.MinTemperature)
            .IsRequired();

        builder.Property(wf => wf.MaxTemperature)
            .IsRequired();

        builder.Property(wf => wf.HasPrecipitation)
            .IsRequired();
    }
}