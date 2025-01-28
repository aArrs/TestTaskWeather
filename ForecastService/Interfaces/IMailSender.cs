using Forecast.DataAccess.Postgress.Models;
using ForecastBackgroundService.Deserialization;
using System.Net.Mail;
using System.Net;

namespace ForecastServices.Interfaces
{
    interface IMailSender
    {
        void SendMail(DevConfig DevConfig, string msgBody);
    }
    class SendMail : IMailSender
    {
        async void IMailSender.SendMail(DevConfig DevConfig, string msgBody)
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
        }
    }
}