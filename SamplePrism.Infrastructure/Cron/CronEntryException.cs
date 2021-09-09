using System;

namespace SamplePrism.Infrastructure.Cron
{
    [Serializable]
    public class CronEntryException : Exception
	{
		public CronEntryException(string message)
			: base(message)
		{

		}
	}
}