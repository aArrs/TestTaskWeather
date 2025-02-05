using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Forecast.DataAccess.Postgress.Models;
namespace Forecast.DataAccess.Postgress.Configurations
{
    public class ForecastDbConfiguration : IEntityTypeConfiguration<ForecastEntity>
    {
        public void Configure(EntityTypeBuilder<ForecastEntity> builder)
        {
            builder.Property(f => f.Id)
                .ValueGeneratedOnAdd();
            builder.Property(f => f.Date)
                .HasColumnName("Date");
            builder.Property(f => f.Temperature)
                .HasColumnName("Temperature");
            builder.Property(f => f.About)
                .HasColumnName("About");
            builder.Property(f => f.Region)
                .HasColumnName("Region");
            builder.Property(f => f.Response)
                .HasColumnName("Response");

        }
    }
}
