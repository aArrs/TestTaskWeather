using Forecast.DataAccess.Postgress.Models;

namespace ForecastServices.Interfaces
{
    interface IMessageBuilder
    {
        string BuildMessage(ForecastEntity forecast);
    }
    class BuildMessage : IMessageBuilder
    {
        string IMessageBuilder.BuildMessage(ForecastEntity forecast)
        {
            string message = $"Date: {forecast.Date}, Temperature: {forecast.Temperature}, Text description: {forecast.About}, Region: {forecast.Region}, Json-response: {forecast.Response}";

            return message;
        }
    }
}
