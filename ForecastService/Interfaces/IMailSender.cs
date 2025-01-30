using Forecast.DataAccess.Postgress.Models;
using ForecastBackgroundService.Deserialization;
using System.Net.Mail;
using System.Net;

namespace ForecastServices.Interfaces
{
    public interface IMailSender
    {
        void SendMail(DevConfig DevConfig, string msgBody);
    }
    class SendMail : IMailSender
    {
        private readonly ILogger<SendMail> _logger;
        public SendMail(ILogger<SendMail> logger)
        {
            _logger = logger;
        }
        async void IMailSender.SendMail(DevConfig DevConfig, string msgBody)
        {
            _logger.LogInformation($"Trying to send mail: {DateTime.Now}");
            try
            {
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
                _logger.LogInformation("Mail is sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Message is not sent due to the following error: {ex.Message}");
            }
        }
    }
}