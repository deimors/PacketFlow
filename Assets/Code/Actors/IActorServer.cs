using System;

namespace PacketFlow.Actors
{
	public interface IActorServer<TEvent, TCommand>
	{
		void SendEvent(TEvent @event);

		IObservable<TCommand> ReceivedCommands { get; }
	}
}
