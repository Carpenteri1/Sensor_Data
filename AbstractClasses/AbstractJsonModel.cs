namespace sensor_data.AbstractClasses
{
	public abstract class AbstractJsonModel
	{
        protected abstract string Timestamp { get; set; }
        protected abstract string SensorName { get; set; }
        protected abstract float? Temperature { get; set; }
        protected abstract float? Humidity { get; set; }
	}
}

