namespace sensor_data.AbstractClasses
{
	public abstract class AbstractSensorDataModel
	{
        public abstract string Timestamp { get; set; }
        public abstract string SensorName { get; set; }
        public abstract uint? Temperature { get; set; }
        public abstract uint? Humidity { get; set; }
    }
}

