using System;

namespace PacketFlow.Domain
{
	public class PacketIdentifier : IEquatable<PacketIdentifier>
	{
		public PacketIdentifier() : this(Guid.NewGuid()) { }

		public PacketIdentifier(Guid value)
		{
			Value = value;
		}

		public Guid Value { get; }

		public override bool Equals(object obj)
		{
			return Equals(obj as PacketIdentifier);
		}

		public bool Equals(PacketIdentifier other) 
			=> other != null && Value.Equals(other.Value);

		public override int GetHashCode() 
			=> -1937169414 + Value.GetHashCode();

		public static bool operator ==(PacketIdentifier identifier1, PacketIdentifier identifier2) 
			=> Equals(identifier1, identifier2);

		public static bool operator !=(PacketIdentifier identifier1, PacketIdentifier identifier2) 
			=> !(identifier1 == identifier2);

		public override string ToString()
			=> $"(Packet {Value})";
	}

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
