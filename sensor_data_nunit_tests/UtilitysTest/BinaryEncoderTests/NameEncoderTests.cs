using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Moq;
using System.Text;
using NUnit.Framework;
using sensor_data.Exceptions;
using sensor_data.Utility;
using sensor_data.Utilitys;
using sensor_data_nunit_tests.Utilitys;

[TestFixture]
public class NameEncoderTests
{
    private readonly string InvalidUUID = "294454-f7c2-11ed-b67e-0242ac1202";
    private readonly string ValidUUID = "a2d50ba2-f7ca-11ed-b67e-0242ac120002";
    private readonly string FirstArgumentValue = "sensor1";
    private readonly string SecondArgumentValue = "sensor2";
    private readonly string NameValue = "sensor1";
    private readonly string ArgumentEmpty = string.Empty;
    private readonly string NameEmpty = string.Empty;
    private const int NameOffset = 13;
    private const int NameLengthOffset = 12;

    private Mock<Encoding> encodingMock =
        new Mock<Encoding>(MockBehavior.Strict);

    [Test]
    public void InValidSensorData_NoNameOrArgument_ReturnArgumentNullException()
    {
        byte[] sensorData = MockData.GetMockSensorData(
            name: NameEmpty,
            temperature: 0,
            humidity: 0);

        MockData.MockSetupForNameEncoder(sensorData,NameEmpty);
        Assert.Throws<ArgumentNullException>(() =>
        BinaryEncoder.NameEncoder(sensorData, ArgumentEmpty));
    }

    [Test]
    public void InValidSensorData_ArgumentDontMatch_NameDidntMatchArgumentException()
    {
        byte[] sensorData = MockData.GetMockSensorData(
               name: NameValue,
               temperature: 0,
               humidity: 0);

        MockData.MockSetupForNameEncoder(sensorData, SecondArgumentValue);
        Assert.Throws<NameDidntMatchArgumentException>(() =>
        BinaryEncoder.NameEncoder(sensorData, SecondArgumentValue));
    }

    [Test]
    public void InValidSensorData_NoValidUUID_ReturnUUIDIsNotValidException()
    {
        byte[] sensorData = MockData.GetMockSensorData(
              name: InvalidUUID,
              temperature: 0,
              humidity: 0);

        MockData.MockSetupForNameEncoder(sensorData, InvalidUUID);
        Assert.Throws<UUIDIsNotValidException>(() =>
        BinaryEncoder.NameEncoder(sensorData, ArgumentEmpty));
    }

    [Test]
    public void ValidSensorData_NoArgument_ReturnsValidUUID()
    {
        byte[] sensorData = MockData.GetMockSensorData(
           name: ValidUUID,
           temperature: 0,
           humidity: 0);

        MockData.MockSetupForNameEncoder(sensorData, ValidUUID);
        string result = BinaryEncoder.NameEncoder(sensorData, ArgumentEmpty);
        Assert.AreEqual(ValidUUID, result);
    }

    [Test]
    public void ValidSensorData_ArgumentMatch_NameArgumentEqual()
    {
        byte[] sensorData = MockData.GetMockSensorData(
                    name: NameValue,
                    temperature: 0,
                    humidity: 0);

        MockData.MockSetupForNameEncoder(sensorData, NameValue);
        string result = BinaryEncoder.NameEncoder(sensorData, FirstArgumentValue);
        Assert.AreEqual(NameValue, result);
    }

}
