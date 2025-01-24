using Forecast.DataAccess.Postgress.Models;
using ForecastBackgroundService.Deserialization;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json;
using System.Reflection.Metadata;


namespace ForecastServices.FunctionalClassess
{
    interface IMessageBuilder
    {
        string BuildMessage(ForecastEntity forecast);
    }
    class BuildMessage: IMessageBuilder
    {
        string IMessageBuilder.BuildMessage(ForecastEntity forecast)
        {
            string message = $"Date: {forecast.Date}, Temperature: {forecast.Temperature}, Text description: {forecast.About}, Region: {forecast.Region}, Json-response: {forecast.Response}";

            return message;
        }
    }
    interface IMailSender
    {
         void SendMail(DevConfig DevConfig, string msgBody);
    }
    class SendMail : IMailSender
    {
        async void IMailSender.SendMail(DevConfig DevConfig, string msgBody)
        {
            WeatherHandler wHandler = new WeatherHandler();

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
    }

    public class MailHandler: DeserializationContract
    {
        public override string JsonString => File.ReadAllText(Path.GetFullPath("appsettings.Development.json"));
        public override DevConfig? DevConfig => JsonConvert.DeserializeObject<DevConfig>(JsonString);

        public async void Main()
        {
            IMessageBuilder _messageBuilder = new BuildMessage();
            IMailSender _mailSender = new SendMail();

            WeatherHandler wHandler = new WeatherHandler();

            string msgBody = _messageBuilder.BuildMessage(await wHandler.Main());
            _mailSender.SendMail(DevConfig, msgBody);
        }
    }
}