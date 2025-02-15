using Forecast.DataAccess.Postgress.Models;
using ForecastServices.Interfaces;
using Microsoft.Extensions.Logging;
using FakeItEasy;

namespace Forecast.Tests
{
    public class MessageBuilderTests
    {
        [Test]
        public void BuildMailResultValue()
        {
            var _logger = A.Fake<ILogger<BuildMessage>>();
            IMessageBuilder _messageBuilder = new BuildMessage(_logger);

            ForecastEntity Forecast = new("2025-01-20 10:52", 22, "Sunny", "Lida", "{Response: Test}");

            string result = _messageBuilder.BuildMessage(Forecast);

            Assert.That(result, Is.EqualTo("Date: 2025-01-20 10:52, Temperature: 22, Text description: Sunny, Region: Lida, Json-response: {Response: Test}"));
        }

        [Test]
        public void BuildMailResultNotNull()
        {
            var _logger = A.Fake<ILogger<BuildMessage>>();
            IMessageBuilder _messageBuilder = new BuildMessage(_logger);

            ForecastEntity Forecast = new("2025-01-20 10:52", 22, "Sunny", "Lida", "{Response: Test}");

            string result = _messageBuilder.BuildMessage(Forecast);

            Assert.NotNull(result);
        }
    }
}