using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace PacketFlow.Actors
{
	public class EventHistory<TEvent> : IObservable<TEvent>
	{
		private readonly ConcurrentQueue<TEvent> _eventHistory = new ConcurrentQueue<TEvent>();

		private readonly ISubject<TEvent> _eventSubject = new Subject<TEvent>();

		public IDisposable Subscribe(IObserver<TEvent> observer)
			=> _eventHistory.ToObservable()
				.Concat(_eventSubject)
				.Subscribe(observer);

		public void CommitEvents(IEnumerable<TEvent> uncommittedEvents)
		{
			uncommittedEvents = uncommittedEvents.ToArray();
			
			foreach (var newEvent in uncommittedEvents)
				_eventHistory.Enqueue(newEvent);
			
			foreach (var newEvent in uncommittedEvents)
				_eventSubject.OnNext(newEvent);
		}
	}
}
