using System;
using sensor_data.Data.DataStrings;

namespace sensor_data.Exceptions
{
	public class NameDidntMatchArgumentException : Exception
	{
        public NameDidntMatchArgumentException(string argument) :
			base(string.Format(
				CustomExceptionMessageStrings.NameDidntMatchNameArgument,
				nameof(argument)))
		{
		}
	}
    public class UUIDIsNotValidException : Exception
    {
        public UUIDIsNotValidException(string UUID) :
            base(string.Format(
                CustomExceptionMessageStrings.IsNotAValidUUID, UUID))
        {
        }
    }
}

