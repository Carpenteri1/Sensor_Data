namespace sensor_data.Models
{
	public class SensorDataModel
	{
        public uint PacketLength { get; set; }
        public string SensorName { get; set; }
        public byte[] RawData { get; set; }
    }
}