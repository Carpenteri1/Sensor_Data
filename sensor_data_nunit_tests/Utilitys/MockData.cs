using System;
using System.Text;
using Moq;

namespace sensor_data_nunit_tests.Utilitys
{
    public static class MockData
    {
        private const int NameOffset = 13;
        private const int NameLengthOffset = 12;
        private const int TemperatureOffset = NameLengthOffset + NameOffset;
        private const int HumidityOffset = TemperatureOffset;
        private const int HumidityOffsetNoTemp = 3;

        public static byte[] GetMockSensorData(string name, float? temperature, uint? humidity)
        {
            const int packetLengthSize = 4;
            const int timestampSize = 8;
            const int nameLengthSize = 1;
            const int temperatureSize = 3;
            const int humiditySize = 2;

            int totalSize = packetLengthSize + timestampSize + nameLengthSize + name.Length;

            if (temperature != null)
                totalSize += temperatureSize;

            if (humidity != null)
                totalSize += humiditySize;

            byte[] mockData = new byte[totalSize];
            byte[] packetLengthBytes = BitConverter.GetBytes((uint)totalSize);

            // Generate and store the timestamp
            DateTimeOffset customTime = new DateTimeOffset(2023, 5, 21, 12, 30, 0, TimeSpan.Zero);
            long timestamp = customTime.ToUnixTimeMilliseconds();
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);
            Array.Copy(timestampBytes, 0, mockData, packetLengthSize, timestampSize);

            byte nameLength = Convert.ToByte(name.Length);
            byte[] nameLengthBytes = new byte[] { nameLength };
            byte[] nameBytes = Encoding.UTF8.GetBytes(name);

            Array.Copy(packetLengthBytes, 0, mockData, 0, packetLengthBytes.Length);
            Array.Copy(nameLengthBytes, 0, mockData, packetLengthBytes.Length + timestampSize, nameLengthBytes.Length);
            Array.Copy(nameBytes, 0, mockData, packetLengthBytes.Length + timestampSize + nameLengthBytes.Length, nameBytes.Length);

            byte[] temperatureBytes = new byte[3];
            if (temperature != null)
            {
                float temperatureValue = (float)temperature;
                temperatureBytes[0] = (byte)((BitConverter.SingleToInt32Bits(temperatureValue) >> 16) & 0xFF);
                temperatureBytes[1] = (byte)((BitConverter.SingleToInt32Bits(temperatureValue) >> 8) & 0xFF);
                temperatureBytes[2] = (byte)(BitConverter.SingleToInt32Bits(temperatureValue) & 0xFF);
                Array.Copy(temperatureBytes, 0, mockData, packetLengthBytes.Length + timestampSize + nameLengthBytes.Length + nameBytes.Length, temperatureBytes.Length);
            }

            if (humidity != null)
            {
                uint humidityValue = (uint)humidity;
                byte[] humidityBytes = BitConverter.GetBytes(humidityValue);
                int humidityOffset = packetLengthBytes.Length + timestampSize + nameLengthBytes.Length + nameBytes.Length;
                Array.Copy(humidityBytes, 0, mockData, humidityOffset, humidityBytes.Length);
            }


            return mockData;
        }


        public static Mock<Encoding> MockSetupForNameEncoder(
            byte[] sensorData, string name)
        {
            Mock<Encoding> encodingMock =
            new Mock<Encoding>(MockBehavior.Strict);

            encodingMock.Setup(e => e.GetString(sensorData, NameOffset,
            GetNameLengthOffset(sensorData)))
            .Returns(name);

            return encodingMock;

        }

        private static byte GetNameLengthOffset(byte[] sensorData) => sensorData[NameLengthOffset];
        public static byte GetNameOffset(byte[] sensorData) =>
        sensorData[NameOffset];
    }
}

