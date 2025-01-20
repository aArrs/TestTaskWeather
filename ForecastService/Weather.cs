using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace ForecastServices
{
    public class Weather
    {
        public Location location { get; set; }
        public Current current { get; set; }

        public Weather(Location location, Current current)
        {
            this.location = location;
            this.current = current;
        }
    }
    public class Location
    {
        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("region")]
        public string region { get; set; }

        [JsonPropertyName("country")]
        public string country { get; set; }

        [JsonPropertyName("lat")]
        public double lat { get; set; }

        [JsonPropertyName("lon")]
        public double lon { get; set; }

        [JsonPropertyName("tz_id")]
        public string tzId { get; set; }

        [JsonPropertyName("localtime_epoch")]
        public int localtimeEpoch { get; set; }

        [JsonPropertyName("localtime")]
        public string date { get; set; }

        public Location(string name, string region, string country, double lat, double lon, string tzId, int localtimeEpoch, string date)
        {
            this.name = name;   
            this.region = region;
            this.country = country;
            this.lat = lat;
            this.lon = lon;
            this.tzId = tzId;
            this.localtimeEpoch = localtimeEpoch;
            this.date = date;
        }
    }
    public class Current
    {
        [JsonPropertyName("temp_c")]
        public double temperature { get; set; }
        public Condition condition { get; set; }

        public Current(double temperature, Condition condition) 
        {
            this.temperature = temperature;
            this.condition = condition;
        }     
    }
    public class Condition
    {
        [JsonPropertyName("text")]
        public string about { get; set; }

        public Condition(string about)
        {
            this.about = about;
        }
    }
}
