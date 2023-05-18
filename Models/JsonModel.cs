using sensor_data.AbstractClasses;

namespace sensor_data.Models
{
    public class JsonModel : AbstractJsonModel
    {
        protected override string Timestamp { get; set; }
        protected override string SensorName { get; set; }
        protected override float? Temperature { get; set; }
        protected override float? Humidity { get; set; }

        public JsonModel(
            string timestamp,
            string sensorname,
            float? temperature,
            float? humidity)
        {
            Timestamp = timestamp;
            SensorName = sensorname;
            Temperature = temperature;
            Humidity = humidity;
        }
    }
}

