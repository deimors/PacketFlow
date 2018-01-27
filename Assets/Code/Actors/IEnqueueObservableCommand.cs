using System;

namespace PacketFlow.Actors
{
	public interface IEnqueueObservableCommand<TCommand, TError>
	{
		IObservable<CommandResult<TError>> ObserveResult(TCommand command);
	}
}
