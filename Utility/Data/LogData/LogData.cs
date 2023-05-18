using Newtonsoft.Json;
using sensor_data.Utility.Data.DataStrings;

namespace sensor_data.Utility.Data.LogData
{
	public static class LogData
	{
		private static string FilePath {get => LogDataStrings.LogFilePath;}
		private static string JsonConvertObject(object dataModel) =>
            JsonConvert.SerializeObject(dataModel, Formatting.None);

		public static bool AppendFileAndWrite(object dataModel)
		{
			try
			{
				var json = JsonConvertObject(dataModel) + Environment.NewLine;
                File.AppendAllText(FilePath,json);
				Console.WriteLine(json);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}

            return true;
        }
    }
}

