using Functional.Maybe;
using Workshop.Core;

namespace PacketFlow.MultiNet.Domain
{
	public class MultiNetAggregate : AggregateRoot<MultiNetEvent, MultiNetState>, IHandleCommand<MultiNetCommand, MultiNetCommand.Error>
	{
		protected MultiNetAggregate() : base(new MultiNetState()) { }

		public Maybe<MultiNetCommand.Error> HandleCommand(MultiNetCommand command)
			=> command.Match(
				SetConnectionState
			);

		private Maybe<MultiNetCommand.Error> SetConnectionState(MultiNetCommand.SetConnectionState command)
			=> this.BuildCommand<MultiNetEvent, MultiNetCommand.Error>()
				.Record(() => new MultiNetEvent.ConnectionStateUpdated(command.NewState))
				.Execute();
	}
}
