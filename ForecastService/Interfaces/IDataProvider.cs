using ForecastBackgroundService.Deserialization;
using ForecastServices.Deserialization;
using System.Net.Http.Json;

namespace ForecastServices.Interfaces
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
}
