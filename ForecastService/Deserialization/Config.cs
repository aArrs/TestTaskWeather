using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForecastService.Deserialization
{
    public class Config
    {
        public Logging logging { get; set; }
        public ConnectionStrings connectionStrings { get; set; }
        public WeatherApiSettings weatherApiSettings { get; set; }
        public MailSettings mailSettings { get; set; }

        public Config(Logging logging, ConnectionStrings connectionStrings, WeatherApiSettings weatherApiSettings, MailSettings mailSettings) 
        {
            this.logging = logging;
            this.connectionStrings = connectionStrings;
            this.weatherApiSettings = weatherApiSettings;
            this.mailSettings = mailSettings;
        }
    }
    public class Logging
    {
        public LogLevel logLevel { get; set; }

        public Logging(LogLevel loglevel) 
        {
            this.logLevel = loglevel;
        }
    }
    public class LogLevel
    {
        [JsonPropertyName("Default")]
        public string def { get; set; }

        [JsonPropertyName("Microsoft.Hosting.Lifetime")]
        public string lifetime { get; set; }

        public LogLevel(string def, string lifetime)
        {
            this.def = def;
            this.lifetime = lifetime;
        }
    }
    public class ConnectionStrings
    {
        [JsonPropertyName("WeatherDbContext")]
        public string dbConnect { get; set; }

        public ConnectionStrings(string dbConnect)
        {
            this.dbConnect = dbConnect;
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
