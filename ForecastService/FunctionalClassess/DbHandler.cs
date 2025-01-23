using Forecast.DataAccess.Postgress.Context;
using Forecast.DataAccess.Postgress.Models;
using ForecastBackgroundService.Deserialization;
using Newtonsoft.Json;

namespace ForecastServices.FunctionalClassess
{
    public class DbHandler : DeserializationContract
    {        
        public override string JsonString => File.ReadAllText(Path.GetFullPath("appsettings.Development.json"));
        public override DevConfig? DevConfig => JsonConvert.DeserializeObject<DevConfig>(JsonString);
        public async void AddToDb()
        {
            using (ForecastDbContext db = new ForecastDbContext())
            {
                ForecastEntity forecast = await WeatherHandler.GetWeatherAsync(DevConfig.weatherApiSettings.reference);

                db.ForecastUnit.Add(forecast);
                db.SaveChanges();
            }
        }

        public static async void GetDbData()
        {
            using (ForecastDbContext db = new ForecastDbContext())
            {
                // получаем объекты из бд и выводим на консоль
                var forecasts = db.ForecastUnit.ToList();
                Console.WriteLine(forecasts);
            }
        }
    }
}
