using System.Net;
using System.Net.Mail;
using Forecast.DataAccess.Postgress.Context;
using Forecast.DataAccess.Postgress.Models;
using System.Net.Http.Json;
using ForecastBackgroundService.Deserialization;
using Newtonsoft.Json;
using Azure;

namespace ForecastServices.FunctionalClassess
{

    interface IEntityProvider
    {
        Task<ForecastEntity> GetEntity(string href);
    }

    public class ForecastEntityProvider : DeserializationContract, IEntityProvider
    {
        static HttpClient httpClient = new HttpClient();
        public async Task<ForecastEntity> GetEntity(string href)
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, href);
            using HttpResponseMessage response = await httpClient.SendAsync(request);

            Weather? content = await response.Content.ReadFromJsonAsync<Weather>();

            ForecastEntity forecast = new ForecastEntity(content.location.date, content.current.temperature, content.current.condition.about, content.location.region, await response.Content.ReadAsStringAsync());

            return forecast;
        }
    }

    public class Program : DeserializationContract
    {
        public override string JsonString => File.ReadAllText(Path.GetFullPath("appsettings.Development.json"));
        public override DevConfig? DevConfig => JsonConvert.DeserializeObject<DevConfig>(JsonString);
        public static async Task<ForecastEntity> Main()
        {
            var program = new Program();
            IEntityProvider entityProvider = new ForecastEntityProvider();
            return await entityProvider.GetEntity(program.DevConfig.weatherApiSettings.reference);
        }
    }
    //public class WeatherHandler : DeserializationContract
    //{
    //    static HttpClient httpClient = new HttpClient();

    //    public static async Task<ForecastEntity> GetWeatherAsync(string href)
    //    {
    //        using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, href);
    //        using HttpResponseMessage response = await httpClient.SendAsync(request);

    //        Weather? content = await response.Content.ReadFromJsonAsync<Weather>();

    //        ForecastEntity forecast = new ForecastEntity(content.location.date, content.current.temperature, content.current.condition.about, content.location.region, await response.Content.ReadAsStringAsync());

    //        return forecast;
    //    }
    //}
}
