using System;
using System.Reflection;
using System.Xml.Linq;
using sensor_data.Models;
using static System.Net.Mime.MediaTypeNames;

namespace sensor_data.Data.DataStrings
{
    public static class BinaryEncoderStrings
    {
        public static readonly string FileName = "./sensor_data.x86_64-apple-darwin";
        public static readonly string DateTimeISO8601Format = "yyyy-MM-ddTHH:mm:sszzz";
    }
    public static class ExceptionMessageStrings
    {
        public static readonly string InvalidSensorData =
            "Error: Invalid sensor data format";
        public static readonly string IsEmptyOrNull = "{0} is empty or null";
        public static string CouldNotLogFile =
            "Error: {0} could not log message: {1}";
        public static readonly string DidntFindNameOrUUID = "Didnt find name or UUID";
    }
    public static class CustomExceptionMessageStrings
    {
        public static readonly string NameDidntMatchNameArgument =
           "Name didnt match --name {0}";
        public static readonly string IsNotAValidUUID =
           "{0} is not a valid UUID";
    }
    public static class MainProgramStrings
    {
        public static readonly string PressAnyKeyToStopSensorProgram =
            "Press any key to stop the sensor program..." + Environment.NewLine;
        public static readonly string WaitingForSensorData =
            "Waiting for sensor data.";
        public static readonly string CompleteDataString =
               "Timestamp: {0}, Sensor Name: {1},";
        public static readonly string EnterNameArgumentOrPressEnter =
       "Enter --name argument or press enter: ";
    }
    public static class LogDataStrings
    {
        private static string ProjectRoot =
            Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory())
                .FullName).Parent.FullName;
        public static string LogsFilePath = Path.Combine(ProjectRoot, "AppData", "Logs");
        public static string LogFileName = "logFile.log";
        public static string FileLogged = "File logged: {0}";
    }
    public static class ProcessBuilderStrings
    {
        public static readonly string NameArgument = "--name {0}";
    }
}
