using Functional.Maybe;
using System;
using static Workshop.Core.AggregateRootExtensions;

namespace Workshop.Core.AggregateCommands
{
	internal class AggregateCommandFailIf<TEvent, TError> : IValidatingAggregateCommand<TEvent, TError>
	{
		private readonly IAggregateCommand<TEvent, TError> _parent;
		private readonly Func<bool> _predicate;
		private readonly Func<TError> _errorFactory;

		public AggregateCommandFailIf(IAggregateCommand<TEvent, TError> parent, Func<bool> predicate, Func<TError> errorFactory)
		{
			_parent = parent;
			_predicate = predicate;
			_errorFactory = errorFactory;
		}

		public Maybe<TError> Execute()
			=> _parent.Execute()
				.SelectOrElse(
					error => error.ToMaybe(),
					() => _predicate()
						? _errorFactory().ToMaybe()
						: Maybe<TError>.Nothing
				);

		public void Record(TEvent @event)
			=> _parent.Record(@event);
	}
}
