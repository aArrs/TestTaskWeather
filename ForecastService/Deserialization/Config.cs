using System.Text.Json.Serialization;

namespace ForecastBackgroundService.Deserialization
{
    public class DevConfig
    {
        public WeatherApiSettings weatherApiSettings { get; set; }
        public MailSettings mailSettings { get; set; }

        public DevConfig(WeatherApiSettings weatherApiSettings, MailSettings mailSettings) 
        {
            this.weatherApiSettings = weatherApiSettings;
            this.mailSettings = mailSettings;
        }
    }
    public class WeatherApiSettings
    {
        [JsonPropertyName("API")]
        public string api { get; set; }

        [JsonPropertyName("Reference")]
        public string reference { get; set; }

        public WeatherApiSettings(string api, string reference)
        {
            this.api = api;
            this.reference = reference;
        }
    }
    public class MailSettings
    {
        [JsonPropertyName("SenderAdress")]
        public string senderAdress { get; set;}

        [JsonPropertyName("SenderAdressPassword")]
        public string senderAdressPassword { get; set; }

        [JsonPropertyName("SenderName")]
        public string senderName { get; set; }

        [JsonPropertyName("RecipientAdress")]
        public string recipientAdress { get; set; }

        [JsonPropertyName("SMTPServer")]
        public string smtpServer { get; set; }

        [JsonPropertyName("Port")]
        public int port { get; set; }

        public MailSettings(string senderAdress, string senderAdressPassword, string senderName, string recipientAdress, string smtpServer, int port)
        {
            this.senderAdress = senderAdress;
            this.senderAdressPassword = senderAdressPassword;
            this.senderName = senderName;
            this.recipientAdress = recipientAdress;
            this.smtpServer = smtpServer;
            this.port = port;
        }
    }

}
