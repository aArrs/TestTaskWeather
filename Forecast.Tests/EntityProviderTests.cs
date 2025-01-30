using Forecast.DataAccess.Postgress.Models;
using ForecastServices.Interfaces;
using Microsoft.Extensions.Logging;
using FakeItEasy;
using ForecastBackgroundService.Deserialization;

namespace Forecast.Tests
{
    public class EntityProviderTests
    {
        static Location location = new Location("Minsk", "Minsk", "Belarus", 53.9, 27.5668, "Europe/Minsk", 1738221506, "2025-01-30 10-10");
        static Condition condition = new Condition("Sunny");
        static Current current = new Current(2.3, condition);
        Weather weather = new Weather(location, current);

        [Fact]
        public async void EntityProviderResultType()
        {
            var _logger = A.Fake<ILogger<EntityProvider>>();
            IEntityProvider _entityProvider = new EntityProvider(_logger);

            Task<ForecastEntity> result = _entityProvider.GetEntity(weather);

            Assert.IsType<ForecastEntity>(await result);
        }

        [Fact]
        public void BuildMailResultNotNull()
        {
            var _logger = A.Fake<ILogger<EntityProvider>>();
            IEntityProvider _entityProvider = new EntityProvider(_logger);

            Task<ForecastEntity> result = _entityProvider.GetEntity(weather);

            Assert.NotNull(result);
        }
    }
}
