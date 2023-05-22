using Newtonsoft.Json;
using sensor_data.Interfaces;

namespace sensor_data.Models
{
    public class JsonModel : IJsonModel
    {
        public string Timestamp { get; set; }
        public string SensorName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float? Temperature { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
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

