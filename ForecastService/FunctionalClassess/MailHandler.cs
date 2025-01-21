using Forecast.DataAccess.Postgress.Models;
using ForecastBackgroundService.Deserialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ForecastServices.FunctionalClassess
{
    public class MailHandler
    {
        static string filepath = Path.GetFullPath("appsettings.Development.json");
        static string jsonString = File.ReadAllText(filepath);
        public static DevConfig? devConfig = JsonConvert.DeserializeObject<DevConfig>(jsonString);
        static string hrefConnect = devConfig.weatherApiSettings.reference;
        public static async Task<string> BuildMail(ForecastEntity forecast)
        {
            string message = $"Date: {forecast.Date}, Temperature: {forecast.Temperature}, Text description: {forecast.About}, Region: {forecast.Region}, Json-response: {forecast.Response}";

            return message;
        }

        public static async void SendMail(DevConfig devConfig)
        {
            string msgBody = await BuildMail(await WeatherHandler.GetWeatherAsync(hrefConnect));

            MailAddress From = new MailAddress(devConfig.mailSettings.senderAdress, devConfig.mailSettings.senderName);
            MailAddress To = new MailAddress(devConfig.mailSettings.recipientAdress);

            MailMessage msg = new MailMessage(From, To);

            msg.Subject = "Weather Forecast";
            msg.Body = $"<p>{msgBody}</p>";
            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(devConfig.mailSettings.smtpServer, devConfig.mailSettings.port);

            smtp.Credentials = new NetworkCredential(devConfig.mailSettings.senderAdress, devConfig.mailSettings.senderAdressPassword);
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }
    }
}
