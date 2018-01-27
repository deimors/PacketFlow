using Functional.Maybe;
using OneOf;
using System.Collections.Generic;
using Workshop.Core;

namespace PacketFlow.Domain
{
	public abstract class NetworkEvent : OneOfBase<NetworkEvent.NodeAdded, NetworkEvent.LinkAdded, NetworkEvent.PacketAdded, NetworkEvent.PacketEnqueued, NetworkEvent.PortAssigned>
	{
		public class NodeAdded : NetworkEvent
		{
			public NodeAdded(Node node)
			{
				Node = node ?? throw new System.ArgumentNullException(nameof(node));
			}

			public Node Node { get; }
		}

		public class LinkAdded : NetworkEvent
		{
			public LinkAdded(Link link)
			{
				Link = link ?? throw new System.ArgumentNullException(nameof(link));
			}

			public Link Link { get; }
		}

		public class PacketAdded : NetworkEvent
		{
			public PacketAdded(Packet packet)
			{
				Packet = packet ?? throw new System.ArgumentNullException(nameof(packet));
			}

			public Packet Packet { get; }
		}

		public class PacketEnqueued : NetworkEvent
		{
			public PacketEnqueued(PacketIdentifier packetId, NodeIdentifier nodeId)
			{
				PacketId = packetId ?? throw new System.ArgumentNullException(nameof(packetId));
				NodeId = nodeId ?? throw new System.ArgumentNullException(nameof(nodeId));
			}

			public PacketIdentifier PacketId { get; }
			public NodeIdentifier NodeId { get; }
		}

		public class PortAssigned : NetworkEvent
		{
			public PortAssigned(NodeIdentifier nodeId, PortDirection port, LinkIdentifier linkId, ConnectionDirection direction)
			{
				NodeId = nodeId ?? throw new System.ArgumentNullException(nameof(nodeId));
				Port = port;
				LinkId = linkId ?? throw new System.ArgumentNullException(nameof(linkId));
				Direction = direction;
			}

			public NodeIdentifier NodeId { get; }
			public PortDirection Port { get; }
			public LinkIdentifier LinkId { get; }
			public ConnectionDirection Direction { get; }
		}
	}

	public abstract class NetworkCommand : OneOfBase<NetworkCommand.AddGatewayNode, NetworkCommand.AddRouterNode, NetworkCommand.AddConsumerNode, NetworkCommand.LinkNodes, NetworkCommand.AddPacket>
	{
		public class AddGatewayNode : NetworkCommand
		{
			public AddGatewayNode(NodeIdentifier nodeId, NodePosition position, int capacity)
			{
				NodeId = nodeId ?? throw new System.ArgumentNullException(nameof(nodeId));
				Position = position ?? throw new System.ArgumentNullException(nameof(position));
				Capacity = capacity;
			}

			public NodeIdentifier NodeId { get; }
			public NodePosition Position { get; }
			public int Capacity { get; }
		}

		public class AddRouterNode : NetworkCommand
		{
			public AddRouterNode(NodeIdentifier nodeId, NodePosition position, int capacity)
			{
				NodeId = nodeId ?? throw new System.ArgumentNullException(nameof(nodeId));
				Position = position ?? throw new System.ArgumentNullException(nameof(position));
				Capacity = capacity;
			}

			public NodeIdentifier NodeId { get; }
			public NodePosition Position { get; }
			public int Capacity { get; }
		}

		public class AddConsumerNode : NetworkCommand
		{
			public AddConsumerNode(NodeIdentifier nodeId, NodePosition position, int capacity)
			{
				NodeId = nodeId ?? throw new System.ArgumentNullException(nameof(nodeId));
				Position = position ?? throw new System.ArgumentNullException(nameof(position));
				Capacity = capacity;
			}

			public NodeIdentifier NodeId { get; }
			public NodePosition Position { get; }
			public int Capacity { get; }
		}

		public class LinkNodes : NetworkCommand
		{
			public LinkNodes(NodeIdentifier source, PortDirection sourcePort, NodeIdentifier sink, PortDirection sinkPort)
			{
				Source = source ?? throw new System.ArgumentNullException(nameof(source));
				SourcePort = sourcePort;
				Sink = sink ?? throw new System.ArgumentNullException(nameof(sink));
				SinkPort = sinkPort;
			}

			public NodeIdentifier Source { get; }
			public PortDirection SourcePort { get; }
			public NodeIdentifier Sink { get; }
			public PortDirection SinkPort { get; }
		}

		public class AddPacket : NetworkCommand
		{
			public AddPacket(PacketIdentifier packedId, PacketType type, NodeIdentifier node)
			{
				PackedId = packedId ?? throw new System.ArgumentNullException(nameof(packedId));
				Type = type;
				NodeId = node ?? throw new System.ArgumentNullException(nameof(node));
			}

			public PacketIdentifier PackedId { get; }
			public PacketType Type { get; }
			public NodeIdentifier NodeId { get; }
		}
	}

	public enum NetworkError
	{
		UnknownNode,
		PacketAlreadyAdded,
		QueueFull,
		PortFull
	}

	public class NetworkState : IApplyEvent<NetworkEvent>
	{
		private readonly Dictionary<NodeIdentifier, Node> _nodes = new Dictionary<NodeIdentifier, Node>();
		private readonly Dictionary<LinkIdentifier, Link> _links = new Dictionary<LinkIdentifier, Link>();
		private readonly Dictionary<PacketIdentifier, Packet> _packets = new Dictionary<PacketIdentifier, Packet>();

		public IReadOnlyDictionary<NodeIdentifier, Node> Nodes { get; }
		public IReadOnlyDictionary<LinkIdentifier, Link> Links { get; }
		public IReadOnlyDictionary<PacketIdentifier, Packet> Packets { get; }

		public void ApplyEvent(NetworkEvent @event)
			=> @event.Switch(
				nodeAdded => _nodes.Add(nodeAdded.Node.Id, nodeAdded.Node),
				linkAdded => _links.Add(linkAdded.Link.Id, linkAdded.Link),
				packedAdded => _packets.Add(packedAdded.Packet.Id, packedAdded.Packet),
				packetEnqueued => EnqueuePacked(packetEnqueued.PacketId, packetEnqueued.NodeId),
				portAssigned => AssignPort(portAssigned)
			);

		private void EnqueuePacked(PacketIdentifier packetId, NodeIdentifier nodeId)
		{
			var node = _nodes[nodeId];
			_nodes[node.Id] = node.With(queue: q => q.Enqueue(packetId));
		}

		private void AssignPort(NetworkEvent.PortAssigned @event)
		{
			var node = _nodes[@event.NodeId];

			_nodes[node.Id] = node.With(ports: p => p.ConnectPort(@event.Port, @event.LinkId, @event.Direction));
		}
	}

	public class NetworkAggregate : AggregateRoot<NetworkEvent, NetworkState>, IHandleCommand<NetworkCommand, NetworkError>
	{
		public NetworkAggregate() : base(new NetworkState()) { }

		public Maybe<NetworkError> HandleCommand(NetworkCommand command)
			=> command.Match(
				AddGatewayNode,
				AddRouterNode,
				AddConsumerNode,
				LinkNodes,
				AddPacket
			);

		private Maybe<NetworkError> AddGatewayNode(NetworkCommand.AddGatewayNode command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.Record(() => new NetworkEvent.NodeAdded(new Node.Gateway(command.NodeId, command.Position, new NodeQueue(command.Capacity), new NodePortSet())))
				.Execute();

		private Maybe<NetworkError> AddRouterNode(NetworkCommand.AddRouterNode command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.Record(() => new NetworkEvent.NodeAdded(new Node.Router(command.NodeId, command.Position, new NodeQueue(command.Capacity), new NodePortSet())))
				.Execute();

		private Maybe<NetworkError> AddConsumerNode(NetworkCommand.AddConsumerNode command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.Record(() => new NetworkEvent.NodeAdded(new Node.Consumer(command.NodeId, command.Position, new NodeQueue(command.Capacity), new NodePortSet())))
				.Execute();

		private Maybe<NetworkError> LinkNodes(NetworkCommand.LinkNodes command)
		{
			var newLinkId = new LinkIdentifier();

			return this.BuildCommand<NetworkEvent, NetworkError>()
				.FailIf(() => !State.Nodes.ContainsKey(command.Source), () => NetworkError.UnknownNode)
				.FailIf(() => !State.Nodes.ContainsKey(command.Sink), () => NetworkError.UnknownNode)
				.FailIf(() => !State.Nodes[command.Source].Ports.IsDisconnected(command.SourcePort), () => NetworkError.PortFull)
				.FailIf(() => !State.Nodes[command.Sink].Ports.IsDisconnected(command.SinkPort), () => NetworkError.PortFull)
				.Record(() => new NetworkEvent.LinkAdded(new Link(newLinkId, command.Source, command.Sink)))
				.Record(() => new NetworkEvent.PortAssigned(command.Source, command.SourcePort, newLinkId, ConnectionDirection.Output))
				.Record(() => new NetworkEvent.PortAssigned(command.Sink, command.SinkPort, newLinkId, ConnectionDirection.Input))
				.Execute();
		}

		private Maybe<NetworkError> AddPacket(NetworkCommand.AddPacket command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.FailIf(() => State.Packets.ContainsKey(command.PackedId), () => NetworkError.PacketAlreadyAdded)
				.FailIf(() => !State.Nodes.ContainsKey(command.NodeId), () => NetworkError.UnknownNode)
				.FailIf(() => State.Nodes[command.NodeId].Queue.IsFull, () => NetworkError.QueueFull)
				.Record(() => new NetworkEvent.PacketAdded(new Packet(command.PackedId, command.Type)))
				.Record(() => new NetworkEvent.PacketEnqueued(command.PackedId, command.NodeId))
				.Execute();
	}
}
