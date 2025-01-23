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
        public override string JsonString => File.ReadAllText(Path.GetFullPath("appsettings.Development.json"));
        public override DevConfig? DevConfig => JsonConvert.DeserializeObject<DevConfig>(JsonString);
        //public override Weather? Weather => base.Weather;
        public static async Task<string> GetResponseMessageAsync(string href)
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, href);
            using HttpResponseMessage response = await httpClient.SendAsync(request);   

            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<ForecastEntity> GetWeatherAsync(string response, Weather weather)
        {
            weather = JsonConvert.DeserializeObject<Weather>(response);

            ForecastEntity forecast = new ForecastEntity(weather.location.date, weather.current.temperature, weather.current.condition.about, weather.location.region, weather.ToString());

            return forecast;
        }
    }
}
