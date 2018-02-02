using System;

namespace PacketFlow.MultiNet.Domain
{
	public class NetworkIdentifier
	{
		public NetworkIdentifier(Guid value)
		{
			Value = value;
		}

		public Guid Value { get; }
	}
}
