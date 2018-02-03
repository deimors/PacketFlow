using System.Collections.Generic;
using Workshop.Core;

namespace PacketFlow.MultiNet.Domain
{
	public class MultiNetState : IApplyEvent<MultiNetEvent>
	{
		private readonly Dictionary<NetworkAddress, Network> _networks = new Dictionary<NetworkAddress, Network>();
		
		public ConnectionState ConnectionState { get; private set; } = new ConnectionState.Disconnected();

		public IReadOnlyDictionary<NetworkAddress, Network> Networks => _networks;

		public void ApplyEvent(MultiNetEvent @event)
			=> @event.Switch(
				connectionStateUpdated => ConnectionState = connectionStateUpdated.NewState
			);
	}
}
