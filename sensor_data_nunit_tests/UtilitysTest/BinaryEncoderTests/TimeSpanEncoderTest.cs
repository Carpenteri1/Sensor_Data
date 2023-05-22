using System;
using sensor_data.Utilitys;

namespace sensor_data_nunit_tests.UtilitysTest.BinaryEncoderTests
{
    using System.Net;
    using NUnit.Framework;
    using NUnit.Framework.Internal;
    using sensor_data.Data.DataStrings;
    using sensor_data_nunit_tests.Utilitys;

    [TestFixture]
    public class TimeStampTests
    {
        private readonly string SensorName = "Sensor1";
        private readonly string ExpectedTimeStamp = "2023-05-21T12:30:00+02:00";

        [Test]
        public void GetTimeStamp_WithValidTimestamp_ReturnsFormattedTimestamp()
        {
            byte[] mockSensorData = MockData.GetMockSensorData(
                name: SensorName,
                temperature: null,
                humidity: null);

            string timestampFormatted = BinaryEncoder.GetTimeStamp(mockSensorData);
            Assert.AreEqual(ExpectedTimeStamp, timestampFormatted);
        }
    }

}