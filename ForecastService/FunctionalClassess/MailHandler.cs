using Forecast.DataAccess.Postgress.Models;
using ForecastBackgroundService.Deserialization;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;


namespace ForecastServices.FunctionalClassess
{

    public class MailHandler : DeserializationContract
    {
        public override string JsonString => File.ReadAllText(Path.GetFullPath("appsettings.Development.json"));
        public override DevConfig? DevConfig => JsonConvert.DeserializeObject<DevConfig>(JsonString);

        public static async Task<string> BuildMail(ForecastEntity forecast)
        {
            string message = $"Date: {forecast.Date}, Temperature: {forecast.Temperature}, Text description: {forecast.About}, Region: {forecast.Region}, Json-response: {forecast.Response}";

            return message;
        }

        public static async void SendMail(DevConfig DevConfig)
        {
            string msgBody = await BuildMail(await WeatherHandler.GetWeatherAsync(DevConfig.weatherApiSettings.reference));

            MailAddress From = new MailAddress(DevConfig.mailSettings.senderAdress, DevConfig.mailSettings.senderName);
            MailAddress To = new MailAddress(DevConfig.mailSettings.recipientAdress);

            MailMessage msg = new MailMessage(From, To);

            msg.Subject = "Weather Forecast";
            msg.Body = $"<p>{msgBody}</p>";
            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(DevConfig.mailSettings.smtpServer, DevConfig.mailSettings.port);

            smtp.Credentials = new NetworkCredential(DevConfig.mailSettings.senderAdress, DevConfig.mailSettings.senderAdressPassword);
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }
        public MailHandler() { }
    }
}
