using System.Net;
using System.Net.Mail;
using Forecast.DataAccess.Postgress.Context;
using Forecast.DataAccess.Postgress.Models;
using System.Net.Http.Json;
using ForecastBackgroundService.Deserialization;
using Newtonsoft.Json;

namespace ForecastServices.FunctionalClassess
{
    public class WeatherHandler : DeserializationContract
    {
        static HttpClient httpClient = new HttpClient();
        
        static string filepath = Path.GetFullPath("appsettings.Development.json");
        static string jsonString = File.ReadAllText(filepath);
        public static DevConfig? devConfig = JsonConvert.DeserializeObject<DevConfig>(jsonString);

        public static async Task<ForecastEntity> GetWeatherAsync(string href)
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, href);
            using HttpResponseMessage response = await httpClient.SendAsync(request);

            Weather? content = await response.Content.ReadFromJsonAsync<Weather>();

            ForecastEntity forecast = new ForecastEntity(content.location.date, content.current.temperature, content.current.condition.about, content.location.region, await response.Content.ReadAsStringAsync());

            return forecast;
        }
    }
}
