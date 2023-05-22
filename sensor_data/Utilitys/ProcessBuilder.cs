using sensor_data.Data.DataStrings;
using System.Diagnostics;

namespace sensor_data.Utility
{
	public static class ProcessBuilder
	{
		public static Process BuildNewProcessStartInfo(string argument)
		{
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = BinaryEncoderStrings.FileName,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            
            if (!string.IsNullOrEmpty(argument))
                startInfo.Arguments =
                    string.Format(ProcessBuilderStrings.NameArgument, argument);

            return Process.Start(startInfo);
        }
	}
}
