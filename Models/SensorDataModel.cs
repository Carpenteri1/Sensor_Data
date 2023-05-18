using sensor_data.AbstractClasses;
namespace sensor_data.Models
{
	public class SensorDataModel : AbstractSensorDataModel
    {
        public override string Timestamp { get; set; }
        public override string SensorName { get; set; }
        public override uint? Temperature { get; set; }
        public override uint? Humidity { get; set; }
   
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
