using System;

namespace PacketFlow.Actors
{
	public interface IActor<TEvent, TCommand> : IObservable<TEvent>, IEnqueueCommand<TCommand> { }
}
