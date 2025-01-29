using Forecast.DataAccess.Postgress.Context;
using Forecast.DataAccess.Postgress.Models;
using Microsoft.Extensions.Logging;

namespace ForecastServices.Interfaces
{
    interface IAddToDb
    {
        void AddToDb(ForecastEntity forecast);
    }
    class AddToDb : IAddToDb
    {
        private readonly ILogger<AddToDb> _logger;
        public AddToDb(ILogger<AddToDb> logger)
        {
            _logger = logger;
        }
        async void IAddToDb.AddToDb(ForecastEntity forecast)
        {
            
            using (ForecastDbContext db = new ForecastDbContext())
            {
                using var transaction = await db.Database.BeginTransactionAsync();
                _logger.LogInformation($"Trying to add entity to the database: {DateTime.Now}");
                try
                {
                    db.ForecastUnit.Add(forecast);
                    _logger.LogInformation("Entity is added successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Entity is not added, error occured: {ex.Message}");
                }
                finally
                {
                    db.SaveChanges();
                    await transaction.CommitAsync();
                    _logger.LogInformation("Changes in database saved successfully");
                }
            }
        }
    }

}
