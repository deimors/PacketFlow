using OneOf;

namespace PacketFlow.MultiNet.Domain
{
	public abstract class ConnectionState : OneOfBase<ConnectionState.Connected, ConnectionState.Connecting, ConnectionState.Disconnected>
	{
		public class Connecting : ConnectionState { }

		public class Connected : ConnectionState
		{
			public Connected(NetworkAddress localAddress)
			{
				LocalAddress = localAddress ?? throw new System.ArgumentNullException(nameof(localAddress));
			}

			public NetworkAddress LocalAddress { get; }
		}

		public class Disconnected : ConnectionState { }
	}
}
