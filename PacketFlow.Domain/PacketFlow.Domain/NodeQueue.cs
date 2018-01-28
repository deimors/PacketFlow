using System;
using System.Collections.Immutable;
using System.Linq;

namespace PacketFlow.Domain
{
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
