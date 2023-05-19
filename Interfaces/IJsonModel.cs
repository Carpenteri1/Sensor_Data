namespace sensor_data.Interfaces
{
	interface IJsonModel
	{
        string Timestamp { get; set; }
        string SensorName { get; set; }
        float? Temperature { get; set; }
        float? Humidity { get; set; }
	}
}