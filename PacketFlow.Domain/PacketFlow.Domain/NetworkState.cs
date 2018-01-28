using System.Collections.Generic;
using Workshop.Core;

namespace PacketFlow.Domain
{
	public class NetworkState : IApplyEvent<NetworkEvent>
	{
		private readonly Dictionary<NodeIdentifier, Node> _nodes = new Dictionary<NodeIdentifier, Node>();
		private readonly Dictionary<LinkIdentifier, Link> _links = new Dictionary<LinkIdentifier, Link>();
		private readonly Dictionary<PacketIdentifier, Packet> _packets = new Dictionary<PacketIdentifier, Packet>();

		public IReadOnlyDictionary<NodeIdentifier, Node> Nodes => _nodes;
		public IReadOnlyDictionary<LinkIdentifier, Link> Links => _links;
		public IReadOnlyDictionary<PacketIdentifier, Packet> Packets => _packets;

		public void ApplyEvent(NetworkEvent @event)
			=> @event.Switch(
				nodeAdded => _nodes.Add(nodeAdded.Node.Id, nodeAdded.Node),
				linkAdded => _links.Add(linkAdded.Link.Id, linkAdded.Link),
				packedAdded => _packets.Add(packedAdded.Packet.Id, packedAdded.Packet),
				packetEnqueued => EnqueuePacked(packetEnqueued.PacketId, packetEnqueued.NodeId),
				portAssigned => AssignPort(portAssigned),
				packetTypeDirectionChanged => ChangePacketTypeDirection(packetTypeDirectionChanged)
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

		private void ChangePacketTypeDirection(NetworkEvent.PacketTypeDirectionChanged @event)
		{
			var routerNode = _nodes[@event.NodeId].AsT1;

			_nodes[routerNode.Id] = routerNode.With(state: s => s.WithPacketDirection(@event.PacketType, @event.Port));
		}
	}
}
