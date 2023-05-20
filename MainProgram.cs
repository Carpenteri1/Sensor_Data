using System.Diagnostics;
using sensor_data.Data.DataStrings;
using sensor_data.Utility;
using sensor_data.Models;
using Newtonsoft.Json;
using sensor_data.Utilitys.LogData;
using sensor_data.Utilitys;

string argument = "";
var sensorProcess = ProcessBuilder.BuildNewProcessStartInfo(argument);
sensorProcess.OutputDataReceived += SensorProcess_OutputDataReceived;
sensorProcess.BeginOutputReadLine();

// Keep the program running until you decide to stop it
Console.WriteLine(MainProgramStrings.WaitingForSensorData);
Console.WriteLine(MainProgramStrings.PressAnyKeyToStopSensorProgram);
Console.ReadKey();

sensorProcess.CloseMainWindow();
sensorProcess.Close();

// Spawn the sensor simulator process and capture its stdout
void SensorProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
{
    try
    {
        if (string.IsNullOrEmpty(e.Data))
            throw new ArgumentException(string.Format(
                    ExceptionMessageStrings.IsEmptyOrNull), nameof(e.Data));

        LogData.CreateFileAndWrite(new JsonModel(
                BinaryEncoder.GetTimeStamp(e),
                BinaryEncoder.NameEncoder(e, argument),
                BinaryEncoder.GetTemperature(e),
                BinaryEncoder.GetHumidity(e)));
    }
    catch (ArgumentOutOfRangeException ex)
    {
        //Ignore
    }
    catch (FormatException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}