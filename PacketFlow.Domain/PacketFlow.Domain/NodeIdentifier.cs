using System;

namespace PacketFlow.Domain
{
	public sealed class NodeIdentifier : IEquatable<NodeIdentifier>
	{
		public NodeIdentifier() : this(Guid.NewGuid()) { }

		public NodeIdentifier(Guid value)
		{
			Value = value;
		}

		public Guid Value { get; }

		public override bool Equals(object obj) 
			=> Equals(obj as NodeIdentifier);

		public bool Equals(NodeIdentifier other) 
			=> other != null && Value.Equals(other.Value);

		public override int GetHashCode() 
			=> -1937169414 + Value.GetHashCode();

		public override string ToString()
			=> $"(Node {Value})";

		public static bool operator ==(NodeIdentifier identifier1, NodeIdentifier identifier2) 
			=> Equals(identifier1, identifier2);

		public static bool operator !=(NodeIdentifier identifier1, NodeIdentifier identifier2) 
			=> !(identifier1 == identifier2);
	}
}
