using Functional.Maybe;

namespace Workshop.Core
{
	public interface IHandleCommand<TCommand, TError>
	{
		Maybe<TError> HandleCommand(TCommand command);
	}
}
