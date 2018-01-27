using Functional.Maybe;
using System;
using static Workshop.Core.AggregateRootExtensions;

namespace Workshop.Core.AggregateCommands
{
	internal class AggregateCommandRoot<TEvent, TError> : IValidatingAggregateCommand<TEvent, TError>
	{
		private readonly IRecordEvent<TEvent> _recorder;

		public AggregateCommandRoot(IRecordEvent<TEvent> recorder)
		{
			_recorder = recorder ?? throw new ArgumentNullException(nameof(recorder));
		}

		public Maybe<TError> Execute()
			=> Maybe<TError>.Nothing;

		public void Record(TEvent @event)
			=> _recorder.Record(@event);
	}
}
