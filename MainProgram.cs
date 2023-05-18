using System.Diagnostics;
using sensor_data.Utility.Data.DataStrings;
using sensor_data.Data.Encoders;
using sensor_data.Models;
using sensor_data.Utility;
using Newtonsoft.Json;
using sensor_data.Utility.Data.LogData;

string argument = "";
var sensorProcess = ProcessBuilder.BuildNewProcessStartInfo(argument);
sensorProcess.OutputDataReceived += SensorProcess_OutputDataReceived;
sensorProcess.BeginOutputReadLine();

// Keep the program running until you decide to stop it
Console.WriteLine($"{MainProgramStrings.WaitingForSensorData}" +
    $"\n{MainProgramStrings.PressAnyKeyToStopSensorProgram}");
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

        var dataModel = new SensorDataModel
        {
            Timestamp = BinaryEncoder.GetTimeStamp(e),
            SensorName = BinaryEncoder.NameEncoder(e, argument),
            Temperature = BinaryEncoder.GetTemperature(e),
            Humidity = BinaryEncoder.GetHumidity(e)
        };
        Console.WriteLine(dataModel.ToString());
        /*
        LogData.AppendFileAndWrite(dataModel);
        string logFileName = $"{dataModel.SensorName}_{DateTime.Now:yyyyMMdd}.json";
        string filePath = Path.Combine(Environment.CurrentDirectory, logFileName);
        string logEntry = dataModel.ToString();*/
        //File.WriteAllText(,);

        //Console.WriteLine(string.Format(MainProgramStrings.CompleteDataString),name);
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