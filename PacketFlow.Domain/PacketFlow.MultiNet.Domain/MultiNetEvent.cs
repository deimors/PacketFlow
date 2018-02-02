using OneOf;
using System;

namespace PacketFlow.MultiNet.Domain
{
	public abstract class MultiNetEvent : OneOfBase<MultiNetEvent.ConnectionStateUpdated>
	{
		public class ConnectionStateUpdated : MultiNetEvent
		{
			public ConnectionStateUpdated(ConnectionState newState)
			{
				NewState = newState ?? throw new ArgumentNullException(nameof(newState));
			}

			public ConnectionState NewState { get; }
		}
	}
}
