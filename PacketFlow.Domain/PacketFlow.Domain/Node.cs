using System;

namespace PacketFlow.Domain
{
	public class NodeIdentifier
	{

	}

	public class NodePosition : IEquatable<NodePosition>
	{
		public NodePosition(float x, float y)
		{
			X = x;
			Y = y;
		}

		public float X { get; }
		public float Y { get; }

		public override bool Equals(object obj) 
			=> Equals(obj as NodePosition);

		public bool Equals(NodePosition other) 
			=> other != null &&
				X == other.X &&
				Y == other.Y;

		public override int GetHashCode()
		{
			var hashCode = 1861411795;
			hashCode = hashCode * -1521134295 + X.GetHashCode();
			hashCode = hashCode * -1521134295 + Y.GetHashCode();
			return hashCode;
		}

		public static bool operator ==(NodePosition position1, NodePosition position2) 
			=> Equals(position1, position2);

		public static bool operator !=(NodePosition position1, NodePosition position2) 
			=> !(position1 == position2);
	}

	public class Node
	{
		public NodeIdentifier Id { get; }
		public NodePosition Position { get; }

		public Node(NodeIdentifier id, NodePosition position)
		{
			Id = id ?? throw new ArgumentNullException(nameof(id));
			Position = position ?? throw new ArgumentNullException(nameof(position));
		}
	}
}
