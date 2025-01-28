using Forecast.DataAccess.Postgress.Models;
using System.Net.Http.Json;
using ForecastBackgroundService.Deserialization;
using Newtonsoft.Json;
using System.Reflection.Metadata;

namespace ForecastServices.FunctionalClassess
{
    interface IDataProvider
    {
        Task<Weather> GetData(HttpClient httpClient, DevConfig devConfig);
    }
    class DataProvider : DeserializationContract, IDataProvider
    {
       
        public async Task<Weather> GetData(HttpClient httpClient, DevConfig devConfig)
        {
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
    class WeatherHandler: DeserializationContract
    {
        private readonly IDataProvider _dataProvider;
        private readonly IEntityProvider _entityProvider;
        private readonly IMessageBuilder _messageBuilder;
        private readonly IMailSender _mailSender;
        private readonly IAddToDb _dbAdder;

        public WeatherHandler(IDataProvider dataProvider, IEntityProvider entityProvider, IMessageBuilder messageBuilder, IMailSender mailSender, IAddToDb dbAdder)
        {
            _dataProvider = dataProvider;
            _entityProvider = entityProvider;
            _messageBuilder = messageBuilder;
            _mailSender = mailSender;
            _dbAdder = dbAdder;
        }

        public override string JsonString => File.ReadAllText(Path.GetFullPath("appsettings.Development.json"));
        public override DevConfig? DevConfig => JsonConvert.DeserializeObject<DevConfig>(JsonString);

        HttpClient httpClient = new HttpClient();

        public async void Main()
        {
            Weather? weather = await _dataProvider.GetData(httpClient, DevConfig);
            ForecastEntity forecast = await _entityProvider.GetEntity(weather);

            _dbAdder.AddToDb(forecast);
            _mailSender.SendMail(DevConfig, _messageBuilder.BuildMessage(forecast));
        }
    }
}
