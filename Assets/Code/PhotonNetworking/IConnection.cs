using OneOf;
using System;

namespace PhotonNetworking
{
	public class ConnectionEvent : OneOfBase<ConnectionEvent.Connecting, ConnectionEvent.Connected, ConnectionEvent.Disconnecting, ConnectionEvent.Disconnected>
	{
		public class Connecting : ConnectionEvent { }
		public class Connected : ConnectionEvent { }
		public class Disconnecting : ConnectionEvent { }
		public class Disconnected : ConnectionEvent { }
	}

	public interface IConnection : IObservable<ConnectionEvent>
	{
		void Connect();
		void Disconnect();
	}
}