using OneOf;

namespace PacketFlow.Actors
{
	public abstract class CommandResult<TError> : OneOfBase<CommandResult<TError>.Success, CommandResult<TError>.Failure>
	{
		public class Success : CommandResult<TError> { }

		public class Failure : CommandResult<TError>
		{
			public TError Error { get; }

			public Failure(TError error)
			{
				Error = error;
			}
		}
	}
}
