using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Workshop.Core;

namespace PacketFlow.Actors
{
	public abstract class Actor<TEvent, TCommand, TError> : IObservable<TEvent>, IEnqueueCommand<TCommand>
	{
		private readonly CommandQueue<TCommand, TError> _commandQueue;

		private readonly EventHistory<TEvent> _history = new EventHistory<TEvent>();

		public Actor(IObservable<Unit> processQueueTicks)
		{
			_commandQueue = new CommandQueue<TCommand, TError>(CommitEvents, OnError);

			processQueueTicks.Subscribe(_ => ProcessQueue());
		}

		public IDisposable Subscribe(IObserver<TEvent> observer)
			=> _history.Subscribe(observer);

		void IEnqueueCommand<TCommand>.Enqueue(TCommand command)
			=> (_commandQueue as IEnqueueCommand<TCommand>).Enqueue(command);

		protected abstract IHandleCommand<TCommand, TError> CommandHandler { get; }

		protected abstract IEnumerable<TEvent> UncommittedEvents { get; }

		protected abstract void MarkCommitted();

		private void ProcessQueue()
			=> _commandQueue.ProcessQueue(CommandHandler);

		private void CommitEvents()
		{
			_history.CommitEvents(UncommittedEvents);
			
			MarkCommitted();
		}

		private void OnError(TError error)
			=> Debug.LogError($"Error: {error.ToString()}");
	}
}
