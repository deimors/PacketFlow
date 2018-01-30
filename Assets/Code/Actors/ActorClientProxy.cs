using System;

namespace PacketFlow.Actors
{
	public class ActorClientProxy<TEvent, TCommand> : IActor<TEvent, TCommand>
	{
		private readonly EventHistory<TEvent> _eventHistory = new EventHistory<TEvent>();
		private readonly IActorGuest<TEvent, TCommand> _client;

		public ActorClientProxy(IActorGuest<TEvent, TCommand> client)
		{
			_client = client;
		}

		public void Enqueue(TCommand command)
		{
			_client.SendCommand(command);
		}

		public IDisposable Subscribe(IObserver<TEvent> observer)
		{
			return _eventHistory.Subscribe(observer);
		}
	}
}
