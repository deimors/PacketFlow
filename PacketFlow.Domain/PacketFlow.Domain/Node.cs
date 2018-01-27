using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Workshop.Core;

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
		public NodeQueue Queue { get; }

		public Node(NodeIdentifier id, NodePosition position, NodeQueue queue)
		{
			Id = id ?? throw new ArgumentNullException(nameof(id));
			Position = position ?? throw new ArgumentNullException(nameof(position));
			Queue = queue ?? throw new ArgumentNullException(nameof(queue));
		}

		public Node With(
			Func<NodePosition, NodePosition> position = null,
			Func<NodeQueue, NodeQueue> queue = null
		) => new Node(
			Id,
			(position ?? Function.Ident)(Position),
			(queue ?? Function.Ident)(Queue)
		);
	}

	public class NodeQueue
	{
		public NodeQueue(int capacity) : this(ImmutableQueue<PacketIdentifier>.Empty, capacity) { }

		public NodeQueue(ImmutableQueue<PacketIdentifier> content, int capacity)
		{
			Content = content ?? throw new ArgumentNullException(nameof(content));
			Capacity = capacity;
		}

		public ImmutableQueue<PacketIdentifier> Content { get; }
		public int Capacity { get; }

		public bool IsFull => Content.Count() == Capacity;

		public NodeQueue Enqueue(PacketIdentifier packetId)
			=> new NodeQueue(Content.Enqueue(packetId), Capacity);
	}
}
