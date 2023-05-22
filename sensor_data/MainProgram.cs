using System.Diagnostics;
using sensor_data.Data.DataStrings;
using sensor_data.Utility;
using sensor_data.Models;
using Newtonsoft.Json;
using sensor_data.Utilitys.LogData;
using sensor_data.Utilitys;
using sensor_data.Exceptions;
using System;
using System.Net;

Console.Write(MainProgramStrings.EnterNameArgumentOrPressEnter);
string argument = Console.ReadLine();
Console.Clear();
Task.Run(async () => await RunSensorProcess(argument));

Console.WriteLine(MainProgramStrings.WaitingForSensorData);
Console.WriteLine(MainProgramStrings.PressAnyKeyToStopSensorProgram);
Console.ReadKey();

void SensorProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
{
    try
    {
        if (string.IsNullOrEmpty(e.Data))
            throw new ArgumentException(string.Format(
                    ExceptionMessageStrings.IsEmptyOrNull), nameof(e.Data));

        var toBytesArray = e.Data.Select(c => (byte)c).ToArray();
        LogData.CreateFileAndWrite(new JsonModel(
                BinaryEncoder.GetTimeStamp(toBytesArray),
                BinaryEncoder.NameEncoder(toBytesArray, argument),
                BinaryEncoder.GetTemperature(toBytesArray),
                BinaryEncoder.GetHumidity(toBytesArray)));
    }
    catch (ArgumentOutOfRangeException ex)
    {
           //ignore
    }
    catch (NameDidntMatchArgumentException ex)
    {
        Console.WriteLine(ex.Message);
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

async Task RunSensorProcess(string argument)
{

    var sensorProcess = ProcessBuilder.BuildNewProcessStartInfo(argument);
    sensorProcess.OutputDataReceived += SensorProcess_OutputDataReceived;
    sensorProcess.BeginOutputReadLine();
    await Task.Run(() => sensorProcess.WaitForExit());
}