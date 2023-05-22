using System;
using sensor_data.Utilitys;
using sensor_data_nunit_tests.Utilitys;

namespace sensor_data_nunit_tests.UtilitysTest.BinaryEncoderTests
{
    public class HumidityEncoderTest
    {

        [Test]
        public void GetHumidity_WithTemperatureAndHumidity_ReturnsHumidityValue()
        {;
            string sensorName = "Sensor1";
            float temperature = 25.5f;
            uint humidity = 50;

            byte[] mockSensorData = MockData.GetMockSensorData(sensorName, temperature, humidity);
            uint? result = BinaryEncoder.GetHumidity(mockSensorData);

            Assert.AreEqual(humidity, result);
        }


        [Test]
        public void GetHumidity_WithNullTemperatureAndNullHumidity_ReturnsNull()
        {
            string sensorName = "Sensor1";
            float? temperature = null;
            uint? humidity = null;

            byte[] mockSensorData = MockData.GetMockSensorData(
                sensorName, temperature, humidity);
            uint? result = BinaryEncoder.GetHumidity(mockSensorData);
            Assert.IsNull(result);
        }

        [Test]
        public void GetHumidity_WithNullTemperatureAndNonNullHumidity_ReturnsHumidityValue()
        {
            string sensorName = "Sensor1";
            float? temperature = null;
            uint humidity = 50;

            byte[] mockSensorData = MockData.GetMockSensorData(sensorName, temperature, humidity);
            uint? result = BinaryEncoder.GetHumidity(mockSensorData);
            Assert.AreEqual(humidity, result);
        }


        [Test]
        public void GetHumidity_WithTemperatureAndNullHumidity_ReturnsHumidityValue()
        {
            string sensorName = "Sensor1";
            float? temperature = 25.5f;
            uint? humidity = null;


            byte[] mockSensorData = MockData.GetMockSensorData(sensorName, temperature, humidity);
            uint? result = BinaryEncoder.GetHumidity(mockSensorData);
            Assert.AreEqual(humidity, result);
        }
    }
}