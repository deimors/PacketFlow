using OneOf;

namespace PacketFlow.MultiNet.Domain
{
	public abstract class ConnectionState : OneOfBase<ConnectionState.Connected, ConnectionState.Connecting, ConnectionState.Disconnected>
	{
		public class Connecting : ConnectionState { }
		public class Connected : ConnectionState { }
		public class Disconnected : ConnectionState { }
	}
}
