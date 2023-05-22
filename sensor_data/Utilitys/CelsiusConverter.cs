namespace sensor_data.Utilitys
{
	public static class CelsiusConverter
	{
        public static float KelvinToCelsius(uint kelvin)
        {
            float celsius = kelvin - 273.15f;
            return celsius;
        }
    }
}

