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

		public enum Error
		{

		}
	}
}
