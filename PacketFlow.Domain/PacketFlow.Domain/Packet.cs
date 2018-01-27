using System;

namespace PacketFlow.Domain
{
	public class PacketIdentifier { }

	public enum PacketType { Red, Blue, Green }

	public class Packet
	{
		public Packet(PacketIdentifier id, PacketType type)
		{
			Id = id ?? throw new ArgumentNullException(nameof(id));
			Type = type;
		}

		public PacketIdentifier Id { get; }
		public PacketType Type { get; }
	}
}
