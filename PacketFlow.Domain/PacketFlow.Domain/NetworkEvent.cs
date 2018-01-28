using OneOf;

namespace PacketFlow.Domain
{
	public abstract class NetworkEvent : OneOfBase<
		NetworkEvent.NodeAdded, 
		NetworkEvent.LinkAdded, 
		NetworkEvent.PacketAdded, 
		NetworkEvent.PacketEnqueued, 
		NetworkEvent.PortAssigned,
		NetworkEvent.PacketTypeDirectionChanged,
		NetworkEvent.PacketTransmissionStarted,
		NetworkEvent.PacketTransmissionFinished,
		NetworkEvent.PacketDequeued
	>
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

		public class PacketTypeDirectionChanged : NetworkEvent
		{
			public PacketTypeDirectionChanged(NodeIdentifier nodeId, PacketType packetType, PortDirection port)
			{
				NodeId = nodeId ?? throw new System.ArgumentNullException(nameof(nodeId));
				PacketType = packetType;
				Port = port;
			}

			public NodeIdentifier NodeId { get; }
			public PacketType PacketType { get; }
			public PortDirection Port { get; }
		}

		public class PacketTransmissionStarted : NetworkEvent
		{
			public PacketTransmissionStarted(PacketIdentifier packetId, LinkIdentifier linkId)
			{
				PacketId = packetId ?? throw new System.ArgumentNullException(nameof(packetId));
				LinkId = linkId ?? throw new System.ArgumentNullException(nameof(linkId));
			}

			public PacketIdentifier PacketId { get; }
			public LinkIdentifier LinkId { get; }
		}

		public class PacketTransmissionFinished : NetworkEvent
		{
			public PacketTransmissionFinished(PacketIdentifier packetId, LinkIdentifier linkId)
			{
				PacketId = packetId ?? throw new System.ArgumentNullException(nameof(packetId));
				LinkId = linkId ?? throw new System.ArgumentNullException(nameof(linkId));
			}

			public PacketIdentifier PacketId { get; }
			public LinkIdentifier LinkId { get; }
		}

		public class PacketDequeued : NetworkEvent
		{
			public PacketDequeued(NodeIdentifier nodeId)
			{
				NodeId = nodeId ?? throw new System.ArgumentNullException(nameof(nodeId));
			}

			public NodeIdentifier NodeId { get; }
		}
	}
}
