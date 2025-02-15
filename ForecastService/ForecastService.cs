﻿using ForecastServices;

namespace ForecastBackgroundService
{
    class ForecastService : BackgroundService
    {
        private readonly WeatherHandler wHandler;
        private readonly ILogger<ForecastService> _logger;

        public ForecastService(WeatherHandler wHandler, ILogger<ForecastService> logger)
        {
            this.wHandler = wHandler;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                wHandler.Main();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
