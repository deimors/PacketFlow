using Functional.Maybe;
using System;
using Workshop.Core.AggregateCommands;

namespace Workshop.Core
{
	public static class AggregateRootExtensions
	{
		public interface IAggregateCommand<TEvent, TError> : IRecordEvent<TEvent>
		{
			Maybe<TError> Execute();
		}

		public interface IValidatingAggregateCommand<TEvent, TError> : IAggregateCommand<TEvent, TError> { }

		public interface IValidatedAggregateCommand<TEvent, TError> : IAggregateCommand<TEvent, TError> { }
		
		public static IValidatingAggregateCommand<TEvent, TError> BuildCommand<TEvent, TError>(this IRecordEvent<TEvent> recorder)
			=> new AggregateCommandRoot<TEvent, TError>(recorder);
		
		public static IValidatingAggregateCommand<TEvent, TError> FailIf<TEvent, TError>(this IValidatingAggregateCommand<TEvent, TError> command, Func<bool> predicate, Func<TError> errorFactory)
			=> new AggregateCommandFailIf<TEvent, TError>(command, predicate, errorFactory);
		
		public static IValidatedAggregateCommand<TEvent, TError> Record<TEvent, TError>(this IAggregateCommand<TEvent, TError> command, Func<TEvent> eventFactory)
			=> new AggregateCommandRecord<TEvent, TError>(command, eventFactory);
		
		public static IValidatedAggregateCommand<TEvent, TError> RecordIf<TEvent, TError>(this IAggregateCommand<TEvent, TError> command, Func<bool> predicate, Func<TEvent> eventFactory)
			=> new AggregateCommandRecordIf<TEvent, TError>(command, predicate, eventFactory);
	}
}
