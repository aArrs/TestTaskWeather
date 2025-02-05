using Forecast.DataAccess.Postgress.Models;
using ForecastServices.Interfaces;
using Microsoft.Extensions.Logging;
using FakeItEasy;
using ForecastBackgroundService.Deserialization;
using Newtonsoft.Json;
using ForecastServices.Deserialization;

namespace Forecast.Tests
{
    public class DataProviderTests: DeserializationContract
    {
        readonly HttpClient httpClient = new HttpClient();
        public override string JsonString => File.ReadAllText(Path.GetFullPath("Config/appsettings.Development.json"));
        public override DevConfig? DevConfig => JsonConvert.DeserializeObject<DevConfig>(JsonString);

        [Fact]
        public async Task DataProviderResultType()
        {
            var _logger = A.Fake<ILogger<DataProvider>>();
            IDataProvider _dataProvider = new DataProvider(_logger);

            Assert.IsType<Weather>(await _dataProvider.GetData(httpClient, DevConfig));
        }

        [Fact]
        public void BuildMailResultNotNull()
        {
            var _logger = A.Fake<ILogger<DataProvider>>();
            IDataProvider _dataProvider = new DataProvider(_logger);

            Assert.NotNull(_dataProvider.GetData(httpClient, DevConfig));
        }
    }
}