using Forecast.DataAccess.Postgress.Models;
using ForecastBackgroundService.Deserialization;

namespace ForecastServices.Interfaces
{
    interface IEntityProvider
    {
        Task<ForecastEntity> GetEntity(Weather weather);
    }
    class EntityProvider : IEntityProvider
    {
        public async Task<ForecastEntity> GetEntity(Weather weather)
        {
            ForecastEntity forecast = new ForecastEntity(weather.location.date, weather.current.temperature, weather.current.condition.about, weather.location.region, System.Text.Json.JsonSerializer.Serialize(weather));

            return forecast;
        }
    }
}
