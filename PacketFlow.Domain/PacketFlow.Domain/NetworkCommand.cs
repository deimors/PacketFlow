using OneOf;

namespace PacketFlow.Domain
{
	public abstract class NetworkCommand : OneOfBase<
		NetworkCommand.AddGatewayNode, 
		NetworkCommand.AddRouterNode, 
		NetworkCommand.AddConsumerNode, 
		NetworkCommand.LinkNodes, 
		NetworkCommand.AddPacket, 
		NetworkCommand.IncrementPacketTypeDirection,
		NetworkCommand.ProcessNodeQueue,
		NetworkCommand.CompleteTransmission
	>
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
			public LinkNodes(NodeIdentifier source, PortDirection sourcePort, NodeIdentifier sink, PortDirection sinkPort, LinkAttributes attributes)
			{
				Source = source ?? throw new System.ArgumentNullException(nameof(source));
				SourcePort = sourcePort;
				Sink = sink ?? throw new System.ArgumentNullException(nameof(sink));
				SinkPort = sinkPort;
				Attributes = attributes;
			}

			public NodeIdentifier Source { get; }
			public PortDirection SourcePort { get; }
			public NodeIdentifier Sink { get; }
			public PortDirection SinkPort { get; }
			public LinkAttributes Attributes { get; }
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

		public class IncrementPacketTypeDirection : NetworkCommand
		{
			public IncrementPacketTypeDirection(NodeIdentifier nodeId, PacketType packetType)
			{
				NodeId = nodeId ?? throw new System.ArgumentNullException(nameof(nodeId));
				PacketType = packetType;
			}

			public NodeIdentifier NodeId { get; }
			public PacketType PacketType { get; }
		}

		public class ProcessNodeQueue : NetworkCommand
		{
			public ProcessNodeQueue(NodeIdentifier nodeId)
			{
				NodeId = nodeId ?? throw new System.ArgumentNullException(nameof(nodeId));
			}

			public NodeIdentifier NodeId { get; }
		}

		public class CompleteTransmission : NetworkCommand
		{
			public CompleteTransmission(PacketIdentifier packetId, LinkIdentifier linkId)
			{
				PacketId = packetId ?? throw new System.ArgumentNullException(nameof(packetId));
				LinkId = linkId ?? throw new System.ArgumentNullException(nameof(linkId));
			}

			public PacketIdentifier PacketId { get; }
			public LinkIdentifier LinkId { get; }
		}
	}
}
