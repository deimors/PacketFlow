using System;

namespace PacketFlow.Actors
{
	public interface IActorGuest<TEvent, TCommand>
	{
		void SendCommand(TCommand command);

		IObservable<TEvent> ReceivedEvents { get; }
	}
}
