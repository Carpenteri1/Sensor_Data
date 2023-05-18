using System;
namespace sensor_data.Data.Encoders
{
	public class CelsiusConverter
	{
		public static float KelvinToCelsiusAsFloat(uint kelvinValue)
		{
            float kelviValueToFloat = kelvinValue; // Implicit conversion from uint to float
            return kelviValueToFloat - 273.15f; // Convert Kelvin to Celsius
        }
        public static uint KelvinToCelsiusAsUInt(uint kelvinValue) =>
            (uint)(kelvinValue - 273.15f);
 
    }
}

