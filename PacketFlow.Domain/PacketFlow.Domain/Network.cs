using Functional.Maybe;
using OneOf;
using System.Collections.Generic;
using Workshop.Core;

namespace PacketFlow.Domain
{
	public abstract class NetworkEvent : OneOfBase<NetworkEvent.NodeAdded, NetworkEvent.LinkAdded, NetworkEvent.PacketAdded, NetworkEvent.PacketEnqueued>
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
	}

	public abstract class NetworkCommand : OneOfBase<NetworkCommand.AddNode, NetworkCommand.LinkNodes, NetworkCommand.AddPacket>
	{
		public class AddNode : NetworkCommand
		{
			public AddNode(NodeIdentifier nodeId, NodePosition position, int capacity)
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
			public LinkNodes(NodeIdentifier source, NodeIdentifier sink)
			{
				Source = source ?? throw new System.ArgumentNullException(nameof(source));
				Sink = sink ?? throw new System.ArgumentNullException(nameof(sink));
			}

			public NodeIdentifier Source { get; }
			public NodeIdentifier Sink { get; }
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
		QueueFull
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
				packetEnqueued => EnqueuePacked(packetEnqueued.PacketId, packetEnqueued.NodeId)
			);

		private void EnqueuePacked(PacketIdentifier packetId, NodeIdentifier nodeId)
		{
			var node = _nodes[nodeId];
			_nodes[nodeId] = node.With(queue: q => q.Enqueue(packetId));
		}
	}

	public class NetworkAggregate : AggregateRoot<NetworkEvent, NetworkState>, IHandleCommand<NetworkCommand, NetworkError>
	{
		public NetworkAggregate() : base(new NetworkState()) { }

		public Maybe<NetworkError> HandleCommand(NetworkCommand command)
			=> command.Match(
				AddNode,
				LinkNodes,
				AddPacket
			);

		private Maybe<NetworkError> AddNode(NetworkCommand.AddNode command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.Record(() => new NetworkEvent.NodeAdded(new Node(command.NodeId, command.Position, new NodeQueue(command.Capacity))))
				.Execute();

		private Maybe<NetworkError> LinkNodes(NetworkCommand.LinkNodes command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.FailIf(() => !State.Nodes.ContainsKey(command.Source), () => NetworkError.UnknownNode)
				.FailIf(() => !State.Nodes.ContainsKey(command.Sink), () => NetworkError.UnknownNode)
				.Record(() => new NetworkEvent.LinkAdded(new Link(new LinkIdentifier(), command.Source, command.Sink)))
				.Execute();

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
