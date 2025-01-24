using Forecast.DataAccess.Postgress.Models;
using System.Net.Http.Json;
using ForecastBackgroundService.Deserialization;
using Newtonsoft.Json;

namespace ForecastServices.FunctionalClassess
{
    interface IDataProvider
    {
        Task<Weather> GetData(DevConfig devConfig);
    }
    class DataProvider : DeserializationContract, IDataProvider
    {
        public async Task<Weather> GetData(DevConfig devConfig)
        {
            HttpClient httpClient = new HttpClient();

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, devConfig.weatherApiSettings.reference);
            using HttpResponseMessage response = await httpClient.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<Weather>();
        }
    }
    interface IEntityProvider
    {
        Task<ForecastEntity> GetEntity(Weather weather);
    }
    class EntityProvider : IEntityProvider
    {
        public async Task<ForecastEntity> GetEntity(Weather weather)
        {
            ForecastEntity forecast = new ForecastEntity(weather.location.date, weather.current.temperature, weather.current.condition.about, weather.location.region, System.Text.Json.JsonSerializer.Serialize(weather));

            return forecast;
        }
    }
    public class WeatherHandler: DeserializationContract
    {
        public override string JsonString => File.ReadAllText(Path.GetFullPath("appsettings.Development.json"));
        public override DevConfig? DevConfig => JsonConvert.DeserializeObject<DevConfig>(JsonString);
        public async Task<ForecastEntity> Main()
        {
            IDataProvider _dataProvider = new DataProvider();
            IEntityProvider _entityProvider = new EntityProvider();

            Weather? weather = await _dataProvider.GetData(DevConfig);
            ForecastEntity forecast = await _entityProvider.GetEntity(weather);

            return forecast;
        }
    }
}
