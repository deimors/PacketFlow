using System.Collections.Generic;
using System.Linq;

namespace Workshop.Core
{
	public abstract class AggregateRoot<TEvent, TState> : IRecordEvent<TEvent>
		where TState : IApplyEvent<TEvent>
	{
		private readonly Queue<TEvent> _uncommittedEvents = new Queue<TEvent>();
		protected readonly TState State;

		protected AggregateRoot(TState state)
		{
			State = state;
		}

		public IEnumerable<TEvent> UncommittedEvents => _uncommittedEvents.AsEnumerable();

		public void MarkCommitted() => _uncommittedEvents.Clear();

		public void Replay(IEnumerable<TEvent> events)
		{
			foreach (var @event in events)
				State.ApplyEvent(@event);
		}

		public void Record(TEvent @event)
		{
			State.ApplyEvent(@event);
			_uncommittedEvents.Enqueue(@event);
		}
	}
}
