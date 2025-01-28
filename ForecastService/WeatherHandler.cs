using Forecast.DataAccess.Postgress.Models;
using ForecastBackgroundService.Deserialization;
using Newtonsoft.Json;
using ForecastServices.Interfaces;
using ForecastServices.Deserialization;

namespace ForecastServices
{
    class WeatherHandler : DeserializationContract
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

        public override string JsonString => File.ReadAllText(Path.GetFullPath("Config/appsettings.Development.json"));
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
