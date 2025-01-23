using Forecast.DataAccess.Postgress.Models;
using ForecastServices.FunctionalClassess;

namespace ForecastBackgroundService
{
    public class ForecastService : BackgroundService
    {
        private readonly ILogger<ForecastService> _logger;
        MailHandler mailHandler = new MailHandler();
        DbHandler dbHandler = new DbHandler();
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
                    dbHandler.AddToDb();
                    MailHandler.SendMail(mailHandler.DevConfig);
                   // WeatherHandler.GetDbData();
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }
    }
}
