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

	public abstract class Node : OneOfBase<Node.Gateway, Node.Router, Node.Consumer>
	{
		public NodeIdentifier Id { get; }
		public NodePosition Position { get; }
		public NodeQueue Queue { get; }
		public NodePortSet Ports { get; }

		public Node(NodeIdentifier id, NodePosition position, NodeQueue queue, NodePortSet ports)
		{
			Id = id ?? throw new ArgumentNullException(nameof(id));
			Position = position ?? throw new ArgumentNullException(nameof(position));
			Queue = queue ?? throw new ArgumentNullException(nameof(queue));
			Ports = ports ?? throw new ArgumentNullException(nameof(ports));
		}

		public abstract Node With(
			Func<NodeQueue, NodeQueue> queue = null,
			Func<NodePortSet, NodePortSet> ports = null
		);

		public class Gateway : Node
		{
			public Gateway(NodeIdentifier id, NodePosition position, NodeQueue queue, NodePortSet ports) : base(id, position, queue, ports)
			{
			}

			public override Node With(
				Func<NodeQueue, NodeQueue> queue = null,
				Func<NodePortSet, NodePortSet> ports = null
			) => new Gateway(
				Id,
				Position,
				(queue ?? Function.Ident)(Queue),
				(ports ?? Function.Ident)(Ports)
			);
		}

		public class Router : Node
		{
			public Router(NodeIdentifier id, NodePosition position, NodeQueue queue, NodePortSet ports, RouterState state) : base(id, position, queue, ports)
			{
				State = state ?? throw new ArgumentNullException(nameof(state));
			}

			public RouterState State { get; }

			public override Node With(
				Func<NodeQueue, NodeQueue> queue = null,
				Func<NodePortSet, NodePortSet> ports = null
			) => new Router(
				Id,
				Position,
				(queue ?? Function.Ident)(Queue),
				(ports ?? Function.Ident)(Ports),
				State
			);

			public Node With(
				Func<RouterState, RouterState> state = null
			) => new Router(
				Id,
				Position,
				Queue,
				Ports,
				(state ?? Function.Ident)(State)
			);

			public PortDirection NextOutputPort(PacketType packetType)
			{
				var outputs = Ports.Outputs.ToArray();
				var currentIndex = outputs.Where(port => port == Ports[State[packetType]]).Select((port, i) => i).FirstMaybe();
				
				return currentIndex.SelectOrElse(
					index => outputs[index + 1 % outputs.Length].Port,
					() => PortDirection.Top
				);
			}
		}

		public class Consumer : Node
		{
			public Consumer(NodeIdentifier id, NodePosition position, NodeQueue queue, NodePortSet ports) : base(id, position, queue, ports)
			{
			}

			public override Node With(
				Func<NodeQueue, NodeQueue> queue = null,
				Func<NodePortSet, NodePortSet> ports = null
			) => new Consumer(
				Id,
				Position,
				(queue ?? Function.Ident)(Queue),
				(ports ?? Function.Ident)(Ports)
			);
		}
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
	
	public class RouterState
	{
		private readonly PortDirection[] _packetTypeToPortMap;

		public RouterState()
		{
			_packetTypeToPortMap = Enumerable.Repeat(PortDirection.Top, Enum.GetValues(typeof(PacketType)).Length).ToArray();
		}

		public RouterState(PortDirection[] packetTypeToPortMap)
		{
			_packetTypeToPortMap = packetTypeToPortMap;
		}

		public PortDirection this[PacketType packetType]
			=> _packetTypeToPortMap[(int)packetType];

		public RouterState WithPacketDirection(PacketType packetType, PortDirection port)
			=> new RouterState(
				_packetTypeToPortMap
					.Select((p, i) => i == (int)packetType ? port : p)
					.ToArray()
			);
	}

	public enum ConnectionDirection { Input, Output }

	public enum PortDirection { Top, Right, Bottom, Left }

	public abstract class NodePort : OneOfBase<NodePort.Disconnected, NodePort.Connected>
	{
		protected NodePort(PortDirection port)
		{
			Port = port;
		}

		public PortDirection Port { get; }

		public class Disconnected : NodePort
		{
			public Disconnected(PortDirection direction) : base(direction)
			{
			}
		}

		public class Connected : NodePort
		{
			public Connected(PortDirection port, LinkIdentifier linkId, ConnectionDirection direction) : base(port)
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
			_ports = Enum.GetValues(typeof(PortDirection)).OfType<PortDirection>().Select(port => new NodePort.Disconnected(port)).Cast<NodePort>().ToArray();
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

		public IEnumerable<NodePort> Inputs
			=> _ports.Where(port => port.Match(_ => false, connected => connected.Direction == ConnectionDirection.Input));

		public IEnumerable<NodePort> Outputs
			=> _ports.Where(port => port.Match(_ => false, connected => connected.Direction == ConnectionDirection.Output));

		public bool IsDisconnected(PortDirection direction)
			=> _ports[(int)direction] is NodePort.Disconnected;

		public NodePortSet ConnectPort(PortDirection port, LinkIdentifier linkId, ConnectionDirection direction)
			=> new NodePortSet(
				_ports
					.Select((p, i) => i == (int)port ? new NodePort.Connected(port, linkId, direction) : p)
					.ToArray()
			);

	}
}
