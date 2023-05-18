namespace sensor_data.Utility.Data.DataStrings
{
	public static class BinaryEncoderStrings
	{
		public static readonly string FileName = "./sensor_data.x86_64-apple-darwin";
		public static readonly string DateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";
    }
	public static class ExceptionMessageStrings
	{
        public static readonly string InvalidSensorData =
            "Error: Invalid sensor data format";
        public static readonly string IsEmptyOrNull = "{0} is empty or null";
    }
	public static class MainProgramStrings
    {
        public static readonly string PressAnyKeyToStopSensorProgram =
            "Press any key to stop the sensor program...";
        public static readonly string WaitingForSensorData =
            "Waiting for sensor data.";
        public static readonly string CompleteDataString =
           	"Timestamp: {0}, Sensor Name: {1},";
    }
    public static class LogDataStrings
    {
        public static readonly string LogFilePath = "";
    }
}
