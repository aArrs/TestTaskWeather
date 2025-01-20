using Forecast.DataAccess.Postgress.Models;
using GetWeatherInfo;

namespace ForecastBackgroundService
{
    public class ForecastService : BackgroundService
    {
        private readonly ILogger<ForecastService> _logger;

        public ForecastService(ILogger<ForecastService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
                while (!stoppingToken.IsCancellationRequested)
                {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    WeatherHandler.AddToDb();
                    WeatherHandler.SendMail();
                    WeatherHandler.GetDbData();
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }
    }
}
