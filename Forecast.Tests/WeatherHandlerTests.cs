using Forecast.DataAccess.Postgress.Models;
using GetWeatherInfo;

namespace Forecast.Tests
{
    public class WeatherHandlerTests
    {
        [Fact]
        public async void BuildMailResultValue()
        {
            ForecastEntity Forecast = new("2025-01-20 10:52", 22, "Sunny", "Lida", "{Response: Test}");

            string result = await WeatherHandler.BuildMail(Forecast);

            Assert.Equal("Date: 2025-01-20 10:52, Temperature: 22, Text description: Sunny, Region: Lida, Json-response: {Response: Test}", result);
        }

        [Fact]
        public async void BuildMailResultNotNull()
        {
            ForecastEntity Forecast = new("2025-01-20 10:52", 22, "Sunny", "Lida", "{Response: Test}");

            string result = await WeatherHandler.BuildMail(Forecast);

            Assert.NotNull(result);
        }
    }
}