using FakeItEasy;
using ForecastBackgroundService.Deserialization;
using ForecastServices.Deserialization;
using ForecastServices.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Forecast.Tests
{
    public class DataProviderTests : DeserializationContract
    {
        readonly HttpClient httpClient = new();
        public override string JsonString => File.ReadAllText(Path.GetFullPath("Config/appsettings.Development.json"));
        public override DevConfig? DevConfig => JsonConvert.DeserializeObject<DevConfig>(JsonString);

        [Test]
        public async Task Test1()
        {
            var _logger = A.Fake<ILogger<DataProvider>>();
            IDataProvider _dataProvider = new DataProvider(_logger);

            Assert.IsInstanceOf<Weather>(await _dataProvider.GetData(httpClient, DevConfig));
        }

        [Test]
        public void BuildMailResultNotNull()
        {
            var _logger = A.Fake<ILogger<DataProvider>>();
            IDataProvider _dataProvider = new DataProvider(_logger);

            Assert.NotNull(_dataProvider.GetData(httpClient, DevConfig));
        }
    }
}