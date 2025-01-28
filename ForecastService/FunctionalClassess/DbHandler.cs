using Forecast.DataAccess.Postgress.Context;
using Forecast.DataAccess.Postgress.Models;
using ForecastBackgroundService.Deserialization;
using Newtonsoft.Json;
using System.Reflection.Metadata;

namespace ForecastServices.FunctionalClassess
{
    interface IAddToDb
    {
        void AddToDb(ForecastEntity forecast);
    }
    class AddToDb : IAddToDb
    {
        async void IAddToDb.AddToDb(ForecastEntity forecast)
        {
            using (ForecastDbContext db = new ForecastDbContext())
            {
                db.ForecastUnit.Add(forecast);
                db.SaveChanges();
            }
        }
    }
    interface IGetDbData
    {
        void GetDbData();
    }
    class GetDbData : IGetDbData
    {
        void IGetDbData.GetDbData()
        {
            using (ForecastDbContext db = new ForecastDbContext())
            {
                // получаем объекты из бд и выводим на консоль
                var forecasts = db.ForecastUnit.ToList();
                Console.WriteLine(forecasts);
            }
        }
    }
    //public class DbHandler
    //{
    //    public DbHandler()
    //    {
            
    //    }
    //    public async void Main()
    //    {
    //    IAddToDb _dbAdder = new AddToDb();
    //    IGetDbData _dataGetter = new GetDbData();

    //    WeatherHandler wHandler = new WeatherHandler();

    //    _dbAdder.AddToDb(await wHandler.Main());
    //    //_dataGetter.GetDbData();
    //    }
    //}
}
