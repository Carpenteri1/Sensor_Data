using System;
using System.Text;
using System.Xml.Linq;
using Moq;

namespace sensor_data_nunit_tests.Utilitys
{
    public static class MockData
    {
        private const int NameOffset = 13;
        private const int NameLengthOffset = 12;
        private const int TemperatureOffset = NameLengthOffset + NameOffset + 3;
        private const int HumidityOffsetNoTemp = NameOffset + NameLengthOffset + 1;
        private const int HumidityOffsetWithTemp = NameOffset + NameLengthOffset + 1 + 3;


        public static byte[] GetMockSensorData(string name, float? temperature, uint? humidity)
        {
            const int packetLengthSize = 4;
            const int timestampSize = 8;
            const int nameLengthSize = 1;
            int nameLength = name.Length;
            int temperatureSize = (temperature != null) ? 3 : 0;
            int humiditySize = (humidity != null) ? 2 : 0;

            int totalSize = packetLengthSize + timestampSize + nameLengthSize + nameLength + temperatureSize + humiditySize;

            byte[] mockData = new byte[totalSize];
            byte[] packetLengthBytes = BitConverter.GetBytes((uint)totalSize);

            // Generate and store the timestamp
            DateTimeOffset customTime = new DateTimeOffset(2023, 5, 21, 12, 30, 0, TimeSpan.Zero);
            long timestamp = customTime.ToUnixTimeMilliseconds();
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);
            Array.Copy(timestampBytes, 0, mockData, packetLengthSize, timestampSize);

            byte nameLengthByte = (byte)nameLength;
            byte[] nameLengthBytes = new byte[] { nameLengthByte };
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

            int temperatureOffset = packetLengthBytes.Length + timestampSize + nameLengthBytes.Length + nameBytes.Length;
            int humidityOffset = (temperature != null) ? temperatureOffset + temperatureSize : HumidityOffsetNoTemp;

            if (humidity != null)
            {
                uint humidityValue = (uint)humidity;
                byte[] humidityBytes = BitConverter.GetBytes(humidityValue);
                int requiredLength = humidityOffset + humidityBytes.Length;

                // Check if the mockData array is long enough
                if (mockData.Length < requiredLength)
                {
                    Array.Resize(ref mockData, requiredLength);
                }

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

