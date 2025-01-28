using Forecast.DataAccess.Postgress.Context;
using Forecast.DataAccess.Postgress.Models;

namespace ForecastServices.Interfaces
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
                try
                {
                    db.ForecastUnit.Add(forecast);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    db.SaveChanges();
                }
            }
        }
    }

}
