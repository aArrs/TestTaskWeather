using ForecastBackgroundService.Deserialization;

namespace ForecastServices.Deserialization
{
    public abstract class DeserializationContract
    {
        public virtual string? JsonString { get; }
        public virtual DevConfig? DevConfig { get; }
        public virtual Weather? Weather { get; }
    }
}
