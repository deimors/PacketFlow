using Functional.Maybe;
using Workshop.Core;

namespace PacketFlow.Actors
{
	public interface ICommandQueueItem<TCommand, TError>
	{
		Maybe<TError> Process(IHandleCommand<TCommand, TError> commandHandler);
	}
}
