using System.Diagnostics;
using sensor_data.Data.DataStrings;
using sensor_data.Data.Encoders;
using sensor_data.Models;
using sensor_data.Services;

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

        Console.WriteLine(new SensorDataModel(
            timeStamp: BinaryEncoder.GetTimeStamp(e),
            sensorName: BinaryEncoder.NameEncoder(e, argument),
            temperature: BinaryEncoder.GetTemperature(e),
            humidity: BinaryEncoder.GetHumidity(e)).ToString());

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

/* TODO
// Output the sensor data to log files
string logFileName = $"{name}_{DateTime.Now:yyyyMMdd}.log";

//string logEntry = $"Timestamp: {timestamp:yyyy-MM-ddTHH:mm:sszzz}, Name: {name}, Temperature: {temperature}°C, Humidity: {humidity}‰";
string logEntry = $"Timestamp: {timestamp}, Name: {name},";

//File.AppendAllText(logFileName, logEntry + Environment.NewLine);
*/