using ForecastBackgroundService.Deserialization;
using ForecastServices.Deserialization;
using System.Net.Http.Json;

namespace ForecastServices.Interfaces
{
    public interface IDataProvider
    {
        Task<Weather> GetData(HttpClient httpClient, DevConfig devConfig);
    }
    public class DataProvider : DeserializationContract, IDataProvider
    {
        private readonly ILogger<DataProvider> _logger;
        public DataProvider(ILogger<DataProvider> logger)
        {
            _logger = logger;
        }
        public async Task<Weather> GetData(HttpClient httpClient, DevConfig devConfig)
        {
            _logger.LogInformation($"Trying to get data from API: {DateTime.Now}");
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, devConfig.weatherApiSettings.reference);
            using HttpResponseMessage response = await httpClient.SendAsync(request);

            return await response.Content.ReadFromJsonAsync<Weather>();
        }
    }
}
