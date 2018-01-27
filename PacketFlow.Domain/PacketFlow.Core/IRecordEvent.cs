namespace Workshop.Core
{
	public interface IRecordEvent<TEvent>
	{
		void Record(TEvent @event);
	}
}
