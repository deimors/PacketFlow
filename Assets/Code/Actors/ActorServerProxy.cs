using System;
using UniRx;

namespace PacketFlow.Actors
{
	public class ActorServerProxy<TEvent, TCommand> : IActor<TEvent, TCommand>
	{
		private readonly ISubject<TEvent> _eventSubject = new Subject<TEvent>();
		private readonly IActor<TEvent, TCommand> _wrapped;

		public ActorServerProxy(IActor<TEvent, TCommand> wrapped, IActorServer<TEvent, TCommand> server)
		{
			_wrapped = wrapped;

			server.ReceivedCommands.Subscribe(Enqueue);
			
			_wrapped.Subscribe(_eventSubject);

			_eventSubject.Subscribe(server.SendEvent);
		}

		public void Enqueue(TCommand command)
		{
			_wrapped.Enqueue(command);
		}

		public IDisposable Subscribe(IObserver<TEvent> observer)
		{
			return _eventSubject.Subscribe(observer);
		}
	}
}
