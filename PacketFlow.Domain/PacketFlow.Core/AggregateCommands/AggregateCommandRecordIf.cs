using Functional.Maybe;
using System;
using static Workshop.Core.AggregateRootExtensions;

namespace Workshop.Core.AggregateCommands
{
	internal class AggregateCommandRecordIf<TEvent, TError> : IValidatedAggregateCommand<TEvent, TError>
	{
		private readonly IAggregateCommand<TEvent, TError> _parent;
		private readonly Func<bool> _predicate;
		private readonly Func<TEvent> _eventFactory;

		public AggregateCommandRecordIf(IAggregateCommand<TEvent, TError> parent, Func<bool> predicate, Func<TEvent> eventFactory)
		{
			_parent = parent;
			_predicate = predicate;
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
			if (_predicate())
				Record(_eventFactory());

			return Maybe<TError>.Nothing;
		}
	}
}
