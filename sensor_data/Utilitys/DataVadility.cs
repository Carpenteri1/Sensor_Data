using System;
using System.Text.RegularExpressions;
using sensor_data.Data.DataStrings;

namespace sensor_data.Utilitys
{
	public static class DataVadility
	{
		public static bool IsValidUUID(string UUID) =>
			new Regex(RegexStrings.RegexUUID_PATTERN)
			.IsMatch(UUID) ? true : false;
	}
}

