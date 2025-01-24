using ForecastServices.FunctionalClassess;

namespace ForecastBackgroundService
{
    public class ForecastService : BackgroundService
    {
        MailHandler mailHandler = new MailHandler();
        DbHandler dbHandler = new DbHandler();

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
                    dbHandler.Main();
                    mailHandler.Main();
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                
                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }
    }
}
