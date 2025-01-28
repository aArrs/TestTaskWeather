using ForecastServices;

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
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    wHandler.Main();                   
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }
    }
}
