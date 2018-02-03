using OneOf;
using System;

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

			public bool Equals(ConnectionStateUpdated other)
			{
				if (ReferenceEquals(null, other)) return false;
				if (ReferenceEquals(this, other)) return true;
				return base.Equals(other) && Equals(NewState, other.NewState);
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != this.GetType()) return false;
				return Equals((ConnectionStateUpdated) obj);
			}

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
