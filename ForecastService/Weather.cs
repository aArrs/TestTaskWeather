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

        public string date { get; set; }
        [JsonPropertyName("date")]

        public string temperature { get; set; }
        [JsonPropertyName("temperature")]

    }
}
