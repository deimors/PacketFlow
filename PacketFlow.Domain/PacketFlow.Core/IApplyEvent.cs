namespace Workshop.Core
{
	public interface IApplyEvent<TEvent>
	{
		void ApplyEvent(TEvent @event);
	}
}
