using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
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
