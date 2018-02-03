using OneOf;
using System;

namespace PacketFlow.MultiNet.Domain
{
	public abstract class MultiNetCommand : OneOfBase<MultiNetCommand.SetConnectionState>
	{
		public class SetConnectionState : MultiNetCommand
		{
			public SetConnectionState(ConnectionState newState)
			{
				NewState = newState ?? throw new ArgumentNullException(nameof(newState));
			}

			public ConnectionState NewState { get; }
		}

		public class AddNetwork : MultiNetCommand
		{
			public AddNetwork(NetworkAddress address)
			{
				Address = address ?? throw new ArgumentNullException(nameof(address));
			}

			public NetworkAddress Address { get; }
		}

		public enum Error
		{
			NetworkAlreadyAdded
		}
	}
}
