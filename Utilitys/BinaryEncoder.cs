﻿using System.Diagnostics;
using System.Net;
using System.Text;
using sensor_data.Utilitys;
using sensor_data.Data.DataStrings;
using sensor_data.Exceptions;

namespace sensor_data.Utilitys
{
    public static class BinaryEncoder
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

            string name = Encoding.UTF8.GetString(
                sensorData,
                NameOffset,
                GetNameLengthOffset(sensorData));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(
                    ExceptionMessageStrings.DidntFindNameOrUUID);

            if (string.IsNullOrEmpty(argument))
                return !DataVadility.IsValidUUID(name) ?
                    throw new UUIDIsNotValidException(argument) : name;

            return argument != name ?
                throw new NameDidntMatchArgumentException(argument) : name;
        }

        public static string GetTimeStamp(DataReceivedEventArgs e)
        {
            long timestampMillisNetworkOrder =
                BitConverter.ToInt64(e.Data.Select(c => (byte)c).ToArray(), 4);

            // Convert timestamp from network byte order to host byte order (little-endian)
            long timestampMillis =
                IPAddress.NetworkToHostOrder(timestampMillisNetworkOrder);
            DateTimeOffset timestamp =
                DateTimeOffset.FromUnixTimeMilliseconds(timestampMillis);
            string timestampFormatted =
                timestamp.DateTime.ToString(
                    BinaryEncoderStrings.DateTimeISO8601Format);

            return timestampFormatted;
        }

        public static uint GetTemperature(DataReceivedEventArgs e)
        {
            byte[] sensorData = e.Data.Select(c => (byte)c).ToArray();

            var tempInKelvin = BitConverter.ToUInt32(
                sensorData,
                GetTemperatureOffset(sensorData));

            return CelsiusConverter.KelvinToCelsiusAsUInt(tempInKelvin);
        }

        //TODO if there is no temp should be 13 + nlen
        public static uint GetHumidity(DataReceivedEventArgs e)
        {
            byte[] sensorData = e.Data.Select(c => (byte)c).ToArray();
            return BitConverter.ToUInt16(sensorData, GetHumidityOffset(sensorData));
        }

        private static byte GetNameLengthOffset(byte[] sensorData) =>
            sensorData[NameLengthOffset];
        private static byte GetTemperatureOffset(byte[] sensorData) =>
            sensorData[TemperatureOffset];
        private static byte GetHumidityOffset(byte[] sensorData) =>
            sensorData[HumidityOffset];

    }
}