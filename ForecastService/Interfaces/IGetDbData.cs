using Forecast.DataAccess.Postgress.Context;

namespace ForecastServices.Interfaces
{
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
}
