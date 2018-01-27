namespace PacketFlow.Actors
{
	public interface IEnqueueCommand<TCommand>
	{
		void Enqueue(TCommand command);
	}
}
