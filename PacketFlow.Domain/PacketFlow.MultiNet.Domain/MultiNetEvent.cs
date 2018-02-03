using OneOf;
using System;
using System.Collections.Generic;

namespace PacketFlow.MultiNet.Domain
{
	public abstract class MultiNetEvent : OneOfBase<MultiNetEvent.ConnectionStateUpdated>
	{
		public class ConnectionStateUpdated : MultiNetEvent, IEquatable<ConnectionStateUpdated>
		{
			public ConnectionStateUpdated(ConnectionState newState)
			{
				NewState = newState ?? throw new ArgumentNullException(nameof(newState));
			}

			public ConnectionState NewState { get; }

			public override bool Equals(object obj)
				=> Equals(obj as ConnectionStateUpdated);

			public bool Equals(ConnectionStateUpdated other)
				=> !(other is null) && Equals(NewState, other.NewState);

			public override int GetHashCode()
			{
				unchecked
				{
					return (base.GetHashCode() * 397) ^ (NewState != null ? NewState.GetHashCode() : 0);
				}
			}
		}
	}
}
