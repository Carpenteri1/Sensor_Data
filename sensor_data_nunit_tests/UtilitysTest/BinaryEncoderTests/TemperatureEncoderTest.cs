using System;
using sensor_data_nunit_tests.Utilitys;
using sensor_data.Utilitys;

namespace sensor_data_nunit_tests.UtilitysTest.BinaryEncoderTests
{
    [TestFixture]
    public class TemperatureEncoderTest
	{
        private readonly string SensorName = "sensor1";
        private readonly float? Temperature = 25;
        private readonly double? Expected = -248.149994f;
        private static int offsetWithoutTemp = 0;        

        [Test]
        public void GetTemperature_WithValidSensorData_ReturnsTemperature()
        {
            byte[] mockSensorData = MockData.GetMockSensorData(
                name: SensorName,
                temperature: Temperature,
                humidity:null);
            var result = BinaryEncoder.GetTemperature(mockSensorData);

            var kelvinValue = BitConverter.ToUInt32(mockSensorData, offsetWithoutTemp);
            var expected = CelsiusConverter
                    .KelvinToCelsius(kelvinValue);

            Assert.AreEqual(expected, result);
        }
    }
}

