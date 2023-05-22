using System.Diagnostics;
using System.Net;
using System.Text;
using sensor_data.Utilitys;
using sensor_data.Data.DataStrings;
using sensor_data.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;



namespace sensor_data.Utilitys
{
    public static class BinaryEncoder
    {
        private const int NameLengthOffset = 12;
        private const int NameOffset = 13;
        private const int TemperatureOffset = NameLengthOffset + NameOffset + 1 + 3;
        private const int HumidityOffsetNoTemp = NameOffset + NameLengthOffset + 1;
        private const int HumidityOffsetWithTemp = TemperatureOffset + 3;
        private const int NoTempOrHumOffset = NameOffset + NameLengthOffset + 1;
        private static bool temperaturePresent;
        private static bool humidityPresent;
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

            uint networkOrder = BitConverter.ToUInt32(data, 0);
            long timestampMillisNetworkOrder =
                IPAddress.NetworkToHostOrder(networkOrder);

            DateTimeOffset timestamp =
                DateTimeOffset.FromUnixTimeMilliseconds(timestampMillisNetworkOrder);
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

        public static uint? GetHumidity(byte[] sensorData )
        {
            temperaturePresent = (sensorData.Length >= TemperatureOffset);
            humidityPresent    = (sensorData.Length >= HumidityOffsetWithTemp);
            //return BitConverter.ToUInt32(sensorData, NoTempOrHumOffset); 

            //We remove temp and humidty so we start from there offset.
            if (!temperaturePresent && humidityPresent)
                return BitConverter.ToUInt32(sensorData, NoTempOrHumOffset);
            else if (temperaturePresent && humidityPresent)
                return BitConverter.ToUInt32(sensorData, NoTempOrHumOffset);
            else if (temperaturePresent && !humidityPresent)
                return BitConverter.ToUInt32(sensorData, NoTempOrHumOffset);

            return null;
        }

        private static byte GetNameLengthOffset(byte[] sensorData) =>
            sensorData[NameLengthOffset];
        private static byte GetTemperatureOffset(byte[] sensorData) =>
            sensorData[TemperatureOffset];
        /*private static byte GetHumidityOffset(byte[] sensorData) =>
            sensorData[HumidityOffsetNoTemp];*/
        private static int GetOffsetWithoutTemp(byte[] sensorData) =>
            NameOffset + GetNameLengthOffset(sensorData);

    }
}