namespace SamplePrism.Infrastructure.Cron
{
	internal class HoursCronEntry : CronEntryBase
	{
		public HoursCronEntry(string expression)
		{
			Initialize(expression, 0, 23);
		}
	}
}