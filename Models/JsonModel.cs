using sensor_data.Interfaces;

namespace sensor_data.Models
{
    public class JsonModel : IJsonModel
    {
        public string Timestamp { get; set; }
        public string SensorName { get; set; }
        public float? Temperature { get; set; }
        public float? Humidity { get; set; }

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

