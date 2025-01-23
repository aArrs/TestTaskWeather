using ForecastBackgroundService.Deserialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastServices.FunctionalClassess
{
    public abstract class DeserializationContract
    {
        public virtual string JsonString { get; }
        public virtual DevConfig? DevConfig { get; }
        public virtual Weather Weather { get; }
    }
}
