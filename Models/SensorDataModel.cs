namespace sensor_data.Models
{
	public class SensorDataModel
	{
        private string Timestamp { get; set; }
        private string SensorName { get; set; }
        private uint? Temperature { get; set; }
        private uint? Humidity { get; set; }

        public SensorDataModel(
            string timeStamp,
            string sensorName,
            uint? temperature,
            uint? humidity)
        {
            Timestamp = timeStamp;
            SensorName = sensorName;
            Temperature = temperature;
            Humidity = humidity;
        }

        public override string ToString()
        {
            return $"" +
                $"Timespan: {Timestamp}, " +
                $"Name: {SensorName}, " +
                $"Temperature: {Temperature} °C, " +
                $"Humidity: {Humidity} %";
        }
    }
}
