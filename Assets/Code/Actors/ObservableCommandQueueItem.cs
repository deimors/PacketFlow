using Functional.Maybe;
using System;
using UniRx;
using Workshop.Core;

namespace PacketFlow.Actors
{
	public class ObservableCommandQueueItem<TCommand, TError> : ICommandQueueItem<TCommand, TError>, IObservable<CommandResult<TError>>
	{
		private readonly ISubject<CommandResult<TError>> _resultSubject = new Subject<CommandResult<TError>>();
		private readonly ICommandQueueItem<TCommand, TError> _inner;

		public ObservableCommandQueueItem(ICommandQueueItem<TCommand, TError> inner)
		{
			_inner = inner;
		}

		public IDisposable Subscribe(IObserver<CommandResult<TError>> observer)
			=> _resultSubject.Subscribe(observer);

		public Maybe<TError> Process(IHandleCommand<TCommand, TError> commandHandler) 
			=> _inner.Process(commandHandler).Match(SendErrorResult, SendSuccessResult);

		private void SendErrorResult(TError error)
		{
			_resultSubject.OnNext(new CommandResult<TError>.Failure(error));
			_resultSubject.OnCompleted();
		}

		private void SendSuccessResult()
		{
			_resultSubject.OnNext(new CommandResult<TError>.Success());
			_resultSubject.OnCompleted();
		}
	}
}
