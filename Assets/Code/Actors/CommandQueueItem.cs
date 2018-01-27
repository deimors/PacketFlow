using Functional.Maybe;
using System;
using Workshop.Core;

namespace PacketFlow.Actors
{
	public class CommandQueueItem<TCommand, TError> : ICommandQueueItem<TCommand, TError>
	{
		private readonly TCommand _command;

		private bool _processed = false;

		public CommandQueueItem(TCommand command)
		{
			_command = command;
		}
		
		public virtual Maybe<TError> Process(IHandleCommand<TCommand, TError> commandHandler)
		{
			if (_processed)
				throw new Exception("Command already processed");

			_processed = true;

			return commandHandler.HandleCommand(_command);	
		}
	}
}
