﻿using Forecast.DataAccess.Postgress.Models;
using ForecastBackgroundService.Deserialization;

namespace ForecastServices.Interfaces
{
    public interface IEntityProvider
    {
        Task<ForecastEntity> GetEntity(Weather weather);
    }
    public class EntityProvider : IEntityProvider
    {
        private readonly ILogger<EntityProvider> _logger;
        public EntityProvider(ILogger<EntityProvider> logger)
        {
            _logger = logger;
        }
        public Task<ForecastEntity> GetEntity(Weather weather)
        {
            _logger.LogInformation($"Trying to create entity for the database: {DateTime.Now}");
            ForecastEntity forecast = new ForecastEntity(weather.location.date, weather.current.temperature, weather.current.condition.about, weather.location.region, System.Text.Json.JsonSerializer.Serialize(weather));

            return Task.FromResult(forecast);
        }
    }
}
