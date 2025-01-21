using System.Net;
using System.Text.Json;
using System.Net.Mail;
using Forecast.DataAccess.Postgress.Context;
using Forecast.DataAccess.Postgress.Models;
using System.Net.Http.Json;
using ForecastService.Deserialization;

namespace GetWeatherInfo
{
    public class WeatherHandler
    {
        static HttpClient httpClient = new HttpClient();
        static string filepath = Path.GetFullPath("appsettings.Development.json");
        static JsonElement config = JsonDocument.Parse(File.ReadAllText(filepath)).RootElement;
        static string hrefConnect = config.GetProperty("WeatherApiSettings").GetProperty("Href").ToString();

        public static async Task<ForecastEntity> GetWeatherAsync(string href)
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, href);
            using HttpResponseMessage response = await httpClient.SendAsync(request);

            Weather content = await response.Content.ReadFromJsonAsync<Weather>();

            ForecastEntity forecast = new ForecastEntity(content.location.date, content.current.temperature,  content.current.condition.about, content.location.region, await response.Content.ReadAsStringAsync());

            return forecast; 
        }

        public static async Task<string> BuildMail(ForecastEntity forecast)
        {
            string message = $"Date: {forecast.Date}, Temperature: {forecast.Temperature}, Text description: {forecast.About}, Region: {forecast.Region}, Json-response: {forecast.Response}";

            return message;
        }
                
        public static async void SendMail()
        {
            var mailSettings = config.GetProperty("MailSettings");
            var senderAdress = mailSettings.GetProperty("SenderAdress").ToString();
            var senderName = mailSettings.GetProperty("SenderName").ToString();
            var senderAdressPassword = mailSettings.GetProperty("SenderAdressPassword").ToString();
            var recipientAdress = mailSettings.GetProperty("RecipientAdress").ToString();
            var smtpServer = mailSettings.GetProperty("SMTPServer").ToString();
            var port = mailSettings.GetProperty("Port").GetInt32();
            
            string msgBody = await BuildMail(await GetWeatherAsync(hrefConnect));

            MailAddress From = new MailAddress(senderAdress, senderName);
            MailAddress To = new MailAddress(recipientAdress);

            MailMessage msg = new MailMessage(From, To);

            msg.Subject = "Weather Forecast";
            msg.Body = $"<p>{msgBody}</p>";
            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(smtpServer, port);

            smtp.Credentials = new NetworkCredential(senderAdress, senderAdressPassword);
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }

        public static async void AddToDb()
        {
            using (ForecastDbContext db = new ForecastDbContext())
            {
                ForecastEntity forecast = await GetWeatherAsync(hrefConnect);

                db.ForecastUnit.Add(forecast);
                db.SaveChanges();
            }
        }

        public static async void GetDbData()
        {
            using (ForecastDbContext db = new ForecastDbContext())
            {
                // получаем объекты из бд и выводим на консоль
                var forecasts = db.ForecastUnit.ToList();
                Console.WriteLine(forecasts);
            }
        }
    }
}
