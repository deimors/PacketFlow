using System;

namespace PacketFlow.Actors
{
	public interface IActorClient<TEvent, TCommand>
	{
		void SendCommand(TCommand command);

		IObservable<TEvent> ReceivedEvents { get; }
	}
}
