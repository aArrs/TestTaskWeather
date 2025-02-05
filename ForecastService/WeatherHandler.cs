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
        private readonly ILogger _logger;
        private readonly IGetDbData _get;

        public WeatherHandler(IDataProvider dataProvider, IEntityProvider entityProvider, IMessageBuilder messageBuilder, IMailSender mailSender, IAddToDb dbAdder, ILogger logger, IGetDbData get)
        {
            _dataProvider = dataProvider;
            _entityProvider = entityProvider;
            _messageBuilder = messageBuilder;
            _mailSender = mailSender;
            _dbAdder = dbAdder;
            _logger = logger;
            _get = get;
        }
        public override string JsonString => File.ReadAllText(Path.GetFullPath("Config/appsettings.Development.json"));
        public override DevConfig? DevConfig => JsonConvert.DeserializeObject<DevConfig>(JsonString);

        readonly HttpClient httpClient = new HttpClient();

        public async void Main()
        {
            _logger.LogInformation($"Worker running at: {DateTime.Now}");
            try
            {
                Weather? weather = await _dataProvider.GetData(httpClient, DevConfig);
                ForecastEntity? forecast = await _entityProvider.GetEntity(weather);
                _dbAdder.AddToDb(forecast);
                _get.GetDbData();
                _mailSender.SendMail(DevConfig, _messageBuilder.BuildMessage(forecast));
            }
            catch(Exception ex)                                                             
            {
                _logger.LogError($"Something went wrong, error text: {ex.Message}");
            }  
        }
    }
}
