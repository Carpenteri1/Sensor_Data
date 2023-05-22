using System.Diagnostics;
using System.Net;
using System.Text;
using sensor_data.Utilitys;
using sensor_data.Data.DataStrings;
using sensor_data.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace sensor_data.Utilitys
{
    public static class BinaryEncoder
    {
        private const int NameLengthOffset = 12;
        private const int NameOffset = 13;
        private const int TemperatureOffset = NameLengthOffset + NameOffset;
        private const int HumidityOffset = TemperatureOffset + 3;
        private static bool temperaturePresent;
        private static int offsetWithoutTemp = 0;

        public static string NameEncoder(byte[] sensorData, string argument)
        {
            // Check if the data contains at least the minimum required number of bytes
            if (sensorData.Length < NameOffset + 1)
                throw new FormatException(ExceptionMessageStrings.InvalidSensorData);

            // Check if the data contains enough bytes to read the name
            if (sensorData.Length < NameOffset + 1 + GetNameLengthOffset(sensorData))
                throw new FormatException(ExceptionMessageStrings.InvalidSensorData);

            string name = Encoding.UTF8.GetString(
                sensorData,
                NameOffset,
                GetNameLengthOffset(sensorData));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(
                    ExceptionMessageStrings.DidntFindNameOrUUID);

            if(!string.IsNullOrEmpty(argument))
                return argument != name ?
                throw new NameDidntMatchArgumentException(argument) : name;

            return DataVadility.IsValidUUID(name) ? name :
                throw new UUIDIsNotValidException(name);
        }

        public static string GetTimeStamp(byte[] data)
        {
            var timestampMillisNetworkOrder = BitConverter.ToInt64(data, 0);
            long timestampMillis =
                 IPAddress.NetworkToHostOrder(timestampMillisNetworkOrder);
            DateTimeOffset timestamp =
                DateTimeOffset.FromUnixTimeMilliseconds(timestampMillis);
            string timestampFormatted =
                timestamp.DateTime.ToString(
                    BinaryEncoderStrings.DateTimeISO8601Format);

            return timestampFormatted;
        }

        public static float? GetTemperature(byte[] sensorData)
        {
            //offsetWithoutTemp = GetOffsetWithoutTemp(sensorData);//TODO maybe remove
            temperaturePresent = (sensorData.Length >= offsetWithoutTemp + 3);
            var kelvinValue = BitConverter.ToUInt32(sensorData, offsetWithoutTemp);

            if (temperaturePresent)
                return CelsiusConverter.KelvinToCelsius(kelvinValue);
           
            return null;
        }

        public static uint? GetHumidity(byte[] sensorData)
        {
            int offsetWithoutTemp = GetOffsetWithoutTemp(sensorData);
            bool temperaturePresent = (sensorData.Length >= offsetWithoutTemp);
            bool humidityPresent = (sensorData.Length >= offsetWithoutTemp + 2);

            if (temperaturePresent)
            {
                return BitConverter.ToUInt32(sensorData, offsetWithoutTemp + 3);
            }
            else if (!temperaturePresent && humidityPresent)
            {
                return BitConverter.ToUInt16(sensorData, offsetWithoutTemp + 1);
            }

            return null;
        }

        private static int GetOffsetWithoutTemp(byte[] sensorData)
        {
            int nameLengthOffset = sensorData[12]; // Add 4 to skip the packet length bytes
            int nameOffset = sensorData[13]; // Add 4 to skip the packet length bytes
            return NameOffset + NameLengthOffset;
        }


        private static byte GetNameLengthOffset(byte[] sensorData) =>
            sensorData[NameLengthOffset];
        private static byte GetTemperatureOffset(byte[] sensorData) =>
            sensorData[TemperatureOffset];
        private static byte GetHumidityOffset(byte[] sensorData) =>
            sensorData[HumidityOffset];

    }
}