using Newtonsoft.Json;
using sensor_data.Models;
using sensor_data.Data.DataStrings;

namespace sensor_data.Utilitys.LogData
{
	public static class LogData
	{
		private static string JsonConvertObject(object dataModel) =>
            JsonConvert.SerializeObject(dataModel, Formatting.None);

		public static bool CreateFileAndWrite(JsonModel jsonModel)
		{ 
			try
			{	
                string json = JsonConvertObject(jsonModel) + Environment.NewLine;
				string filePath = Path.Combine(LogDataStrings.LogsFilePath,
					LogDataStrings.LogFileName);

				File.AppendAllText(filePath,json);
				Console.WriteLine(string.Format(LogDataStrings.FileLogged,json));
			}
			catch (DirectoryNotFoundException e)
			{
				Console.WriteLine(string.Format(
					ExceptionMessageStrings.CouldNotLogFile,
					jsonModel.SensorName, e.Message));
                return false;
            }
            catch (Exception e)
			{
				Console.WriteLine(string.Format(
					ExceptionMessageStrings.CouldNotLogFile,
                    jsonModel.SensorName,
					e.Message));
				return false;
			}

            return true;
        }
    }
}

