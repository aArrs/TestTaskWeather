using System.Text.Json;
using System.Text.Json.Serialization;

namespace Forecast.DataAccess.Postgress.Deserialization
{
    public class Config
    {
        public ConnectionStrings connectionStrings { get; set; }

        public Config(ConnectionStrings connectionStrings) 
        {
            this.connectionStrings = connectionStrings;
        }
    }
    public class ConnectionStrings
    {
        [JsonPropertyName("WeatherDbContext")]
        public string dbConnect { get; set; }

        public ConnectionStrings(string dbConnect)
        {
            this.dbConnect = dbConnect;
        }
    }
}
