using System;

namespace PacketFlow.Actors
{
	public interface IActorHost<TEvent, TCommand>
	{
		void SendEvent(TEvent @event);

		IObservable<TCommand> ReceivedCommands { get; }
	}
}
