using System.Diagnostics;
using System.Net;
using System.Text;
using sensor_data.Data.DataStrings;

namespace sensor_data.Data.Encoders
{
    public class BinaryEncoder
    {
        const int PacketLengthOffset = 0;
        private const int NameLengthOffset = 12;
        private const int NameOffset = 13;
        private const int TemperatureOffset = NameLengthOffset + NameOffset;
        private const int HumidityOffset = TemperatureOffset + 3;

        public static string NameEncoder(DataReceivedEventArgs e, string argument)
        {
            byte[] sensorData = e.Data.Select(c => (byte)c).ToArray();
            // Check if the data contains at least the minimum required number of bytes
            if (sensorData.Length < NameOffset + 1)
                throw new FormatException(ExceptionMessageStrings.InvalidSensorData);

            // Check if the data contains enough bytes to read the name
            if (sensorData.Length < NameOffset + 1 + GetNameLengthOffset(sensorData))
                throw new FormatException(ExceptionMessageStrings.InvalidSensorData);

            //TODO Try extract name using ASCII encoding if not working
            string name = Encoding.UTF8.GetString(
                sensorData,
                NameOffset,
                GetNameLengthOffset(sensorData));

            //TODO make custom exception here.
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("name is null for test only");

            //TODO need to extract argument value from name using regex
            //if (argument != name)
            //   throw new ArgumentException();
            return name;
        }

        public static string GetTimeStamp(DataReceivedEventArgs e)
        {
            long timestampMillisNetworkOrder = BitConverter.ToInt64(e.Data.Select(c => (byte)c).ToArray(), 4);

            // Convert timestamp from network byte order to host byte order (little-endian)
            long timestampMillis = IPAddress.NetworkToHostOrder(timestampMillisNetworkOrder);
            DateTime timestamp = DateTimeOffset.FromUnixTimeMilliseconds(timestampMillis).DateTime;
            string timestampFormatted = timestamp.ToString(BinaryEncoderStrings.DateTimeFormat);

            return timestampFormatted;
        }

        public static uint GetTemperature(DataReceivedEventArgs e)
        {
            byte[] sensorData = e.Data.Select(c => (byte)c).ToArray();
            var tempInKelvin = BitConverter.ToUInt32(sensorData, GetTemperatureOffset(sensorData));
            return CelsiusConverter.KelvinToCelsiusAsUInt(tempInKelvin);
        }

        public static uint GetHumidity(DataReceivedEventArgs e)
        {
            byte[] sensorData = e.Data.Select(c => (byte)c).ToArray();
            return BitConverter.ToUInt16(sensorData, GetHumidityOffset(sensorData));
        }

        private static byte GetNameLengthOffset(byte[] sensorData) => sensorData[NameLengthOffset];
        private static byte GetTemperatureOffset(byte[] sensorData) => sensorData[TemperatureOffset];
        private static byte GetHumidityOffset(byte[] sensorData) => sensorData[HumidityOffset];

    }
}