using Workshop.Core;

namespace PacketFlow.MultiNet.Domain
{
	public class MultiNetState : IApplyEvent<MultiNetEvent>
	{
		public ConnectionState ConnectionState { get; private set; } = new ConnectionState.Disconnected();

		public void ApplyEvent(MultiNetEvent @event)
			=> @event.Switch(
				connectionStateUpdated => ConnectionState = connectionStateUpdated.NewState
			);
	}
}
