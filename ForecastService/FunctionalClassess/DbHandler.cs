using Forecast.DataAccess.Postgress.Context;
using Forecast.DataAccess.Postgress.Models;
using ForecastBackgroundService.Deserialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastServices.FunctionalClassess
{
    public class DbHandler
    {
        static string filepath = Path.GetFullPath("appsettings.Development.json");
        static string jsonString = File.ReadAllText(filepath);
        public static DevConfig? devConfig = JsonConvert.DeserializeObject<DevConfig>(jsonString);
        static string hrefConnect = devConfig.weatherApiSettings.reference;
        public static async void AddToDb()
        {
            using (ForecastDbContext db = new ForecastDbContext())
            {
                ForecastEntity forecast = await WeatherHandler.GetWeatherAsync(hrefConnect);

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
