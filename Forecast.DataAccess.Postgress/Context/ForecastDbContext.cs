using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Forecast.DataAccess.Postgress.Deserialization;
using System.Text.Json;
using Forecast.DataAccess.Postgress.Models;

namespace Forecast.DataAccess.Postgress.Context
{
    public class ForecastDbContext : DbContext
    {
        public DbSet<ForecastEntity> ForecastUnit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string filepath = Path.GetFullPath("dbConnectionSettings.json");
            var connectProps = JsonDocument.Parse(File.ReadAllText(filepath)).RootElement.GetProperty("ConnectionStrings").GetProperty("WeatherDbContext").ToString();

            optionsBuilder.UseNpgsql(connectProps);
        }
    }
}
