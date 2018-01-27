using Functional.Maybe;
using System;
using System.Collections.Concurrent;
using UnityEngine;
using Workshop.Core;

namespace PacketFlow.Actors
{
	public class CommandQueue<TCommand, TError> : IEnqueueCommand<TCommand>, IEnqueueObservableCommand<TCommand, TError>
	{
		private readonly ConcurrentQueue<ICommandQueueItem<TCommand, TError>> _commandQueue = new ConcurrentQueue<ICommandQueueItem<TCommand, TError>>();

		private readonly Action commit;
		private readonly Action<TError> onError;

		public CommandQueue(Action commit, Action<TError> onError)
		{
			this.commit = commit;
			this.onError = onError;
		}

		void IEnqueueCommand<TCommand>.Enqueue(TCommand command)
			=> _commandQueue.Enqueue(new CommandQueueItem<TCommand, TError>(command));

		IObservable<CommandResult<TError>> IEnqueueObservableCommand<TCommand, TError>.ObserveResult(TCommand command)
		{
			var queueItem = new ObservableCommandQueueItem<TCommand, TError>(new CommandQueueItem<TCommand, TError>(command));
			_commandQueue.Enqueue(queueItem);
			return queueItem;
		}

		public void ProcessQueue(IHandleCommand<TCommand, TError> commandHandler)
		{
			ICommandQueueItem<TCommand, TError> queueItem;

			while (_commandQueue.TryDequeue(out queueItem))
				queueItem
					.Process(commandHandler)
					.Match(onError, commit);
		}
	}
}
