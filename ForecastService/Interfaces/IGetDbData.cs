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
                try
                {
                    var forecasts = db.ForecastUnit.ToList();
                    Console.WriteLine(forecasts);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
