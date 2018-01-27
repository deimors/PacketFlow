using Functional.Maybe;
using OneOf;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Workshop.Core;

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

		public override string ToString()
			=> $"({X}, {Y})";
	}

	public class Node
	{
		public NodeIdentifier Id { get; }
		public NodePosition Position { get; }
		public NodeQueue Queue { get; }
		public NodeType Type { get; }
		public NodePortSet Ports { get; }

		public Node(NodeIdentifier id, NodePosition position, NodeQueue queue, NodeType type, NodePortSet ports)
		{
			Id = id ?? throw new ArgumentNullException(nameof(id));
			Position = position ?? throw new ArgumentNullException(nameof(position));
			Queue = queue ?? throw new ArgumentNullException(nameof(queue));
			Type = type ?? throw new ArgumentNullException(nameof(type));
			Ports = ports ?? throw new ArgumentNullException(nameof(ports));
		}

		public Node With(
			Func<NodePosition, NodePosition> position = null,
			Func<NodeQueue, NodeQueue> queue = null,
			Func<NodeType, NodeType> type = null,
			Func<NodePortSet, NodePortSet> ports = null
		) => new Node(
			Id,
			(position ?? Function.Ident)(Position),
			(queue ?? Function.Ident)(Queue),
			(type ?? Function.Ident)(Type),
			(ports ?? Function.Ident)(Ports)
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

	public abstract class NodeType : OneOfBase<NodeType.Gateway, NodeType.Router, NodeType.Consumer>
	{
		public class Gateway : NodeType
		{
			
		}

		public class Router : NodeType
		{

		}

		public class Consumer : NodeType
		{

		}
	}

	public enum ConnectionDirection { Input, Output }

	public enum PortDirection { Top, Right, Bottom, Left }

	public abstract class NodePort : OneOfBase<NodePort.Disconnected, NodePort.Connected>
	{
		public class Disconnected : NodePort { }

		public class Connected : NodePort
		{
			public Connected(LinkIdentifier linkId, ConnectionDirection direction)
			{
				LinkId = linkId ?? throw new ArgumentNullException(nameof(linkId));
				Direction = direction;
			}

			public LinkIdentifier LinkId { get; }
			public ConnectionDirection Direction { get; }
		}
	}

	public class NodePortSet
	{
		private readonly NodePort[] _ports; 

		public NodePortSet()
		{
			_ports = Enumerable.Repeat(new NodePort.Disconnected(), Enum.GetValues(typeof(PortDirection)).Length).OfType<NodePort>().ToArray();
		}

		public NodePortSet(NodePort[] ports)
		{
			_ports = ports;
		}

		public NodePort this[PortDirection direction]
		{
			get
			{
				return _ports[(int)direction];
			}
		}

		public bool IsDisconnected(PortDirection direction)
			=> _ports[(int)direction] is NodePort.Disconnected;

		public NodePortSet ConnectPort(PortDirection port, LinkIdentifier linkId, ConnectionDirection direction)
			=> new NodePortSet(
				_ports
					.Select((p, i) => i == (int)port ? new NodePort.Connected(linkId, direction) : p)
					.ToArray()
			);

	}
}
