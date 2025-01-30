using Forecast.DataAccess.Postgress.Models;

namespace ForecastServices.Interfaces
{
    public interface IMessageBuilder
    {
        string BuildMessage(ForecastEntity forecast);
    }
    public class BuildMessage : IMessageBuilder
    {
        private readonly ILogger<BuildMessage> _logger;

        public BuildMessage(ILogger<BuildMessage> logger)
        {
            _logger = logger;
        }
        string IMessageBuilder.BuildMessage(ForecastEntity forecast)
        {
            _logger.LogInformation($"Trying to build message at: {DateTime.Now}");
            string message = $"Date: {forecast.Date}, Temperature: {forecast.Temperature}, Text description: {forecast.About}, Region: {forecast.Region}, Json-response: {forecast.Response}";
            _logger.LogInformation($"The following message is built successfully: {message}");

            return message;
        }
    }
}
