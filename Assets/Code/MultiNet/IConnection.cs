﻿using OneOf;
using System;

namespace MultiNet
{
	public class ConnectionEvent : OneOfBase<ConnectionEvent.Connecting, ConnectionEvent.Connected, ConnectionEvent.Disconnecting, ConnectionEvent.Disconnected>
	{
		public class Connecting : ConnectionEvent { }
		public class Connected : ConnectionEvent { }
		public class Disconnecting : ConnectionEvent { }
		public class Disconnected : ConnectionEvent { }
	}

	public interface IConnection
	{
		void Connect();
		void Disconnect();

		IObservable<ConnectionEvent> Events { get; }
	}
}