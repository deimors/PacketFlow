using Functional.Maybe;
using System;
using static Workshop.Core.AggregateRootExtensions;

namespace Workshop.Core.AggregateCommands
{
	internal class AggregateCommandRecord<TEvent, TError> : IValidatedAggregateCommand<TEvent, TError>
	{
		private readonly IAggregateCommand<TEvent, TError> _parent;
		private readonly Func<TEvent> _eventFactory;

		public AggregateCommandRecord(IAggregateCommand<TEvent, TError> parent, Func<TEvent> eventFactory)
		{
			_parent = parent;
			_eventFactory = eventFactory;
		}

		public Maybe<TError> Execute()
			=> _parent.Execute()
				.SelectOrElse(
					error => error.ToMaybe(),
					Succeed
				);

		public void Record(TEvent @event)
			=> _parent.Record(@event);

		private Maybe<TError> Succeed()
		{
			Record(_eventFactory());
			return Maybe<TError>.Nothing;
		}
	}
}
