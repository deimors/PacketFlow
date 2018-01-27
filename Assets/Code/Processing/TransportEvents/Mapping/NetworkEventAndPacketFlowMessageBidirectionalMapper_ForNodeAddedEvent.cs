using System;
using System.Collections.Immutable;
using System.Linq;
using PacketFlow.Domain;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{
		private const int NODE_TYPE_GATEWAY = 0;
		private const int NODE_TYPE_ROUTER = 1;
		private const int NODE_TYPE_CONSUMER = 2;

		private static object ToNodeAddedEventTransport(this NetworkEvent.NodeAdded source)
		{
			return new NodeAddedEventTransport
			{
				NodeID = source.Node.Id.Value,
				NodeType = source.Node.Type.Match(gateway => NODE_TYPE_GATEWAY, router => NODE_TYPE_ROUTER, consumer => NODE_TYPE_CONSUMER),
				PositionX = source.Node.Position.X,
				PositionY = source.Node.Position.Y,
				QueueContent = source.Node.Queue.Content.Select(x => x.Value).ToArray(),
				QueueCapacity = source.Node.Queue.Capacity,
				TopPortLinkIdentifier = source.Node.Ports.GetLinkIdentifier(PortDirection.Top),
				TopPortConnectionDirection = source.Node.Ports.GetLinkDirection(PortDirection.Top),
				BottomPortLinkIdentifier = source.Node.Ports.GetLinkIdentifier(PortDirection.Bottom),
				BottomPortConnectionDirection = source.Node.Ports.GetLinkDirection(PortDirection.Bottom),
				LeftPortLinkIdentifier = source.Node.Ports.GetLinkIdentifier(PortDirection.Left),
				LeftPortConnectionDirection = source.Node.Ports.GetLinkDirection(PortDirection.Left),
				RightPortLinkIdentifier = source.Node.Ports.GetLinkIdentifier(PortDirection.Right),
				RightPortConnectionDirection = source.Node.Ports.GetLinkDirection(PortDirection.Right)
			};
		}

		#region ... details for ToNodeAddedEventTransport

		private static Guid GetLinkIdentifier(this NodePortSet nodePortSet, PortDirection direction)
		{
			return nodePortSet[direction].Match(disconnected => Guid.Empty, connected => connected.LinkId.Value);
		}

		private static int GetLinkDirection(this NodePortSet nodePortSet, PortDirection direction)
		{
			return nodePortSet[direction].Match(disconnected => -1, connected => (int)connected.Direction);
		}

		#endregion

		private static NetworkEvent ToNetworkEvent(this NodeAddedEventTransport transport)
		{
			return new NetworkEvent.NodeAdded(
				new Node(new NodeIdentifier(transport.NodeID),
					new NodePosition(transport.PositionX, transport.PositionY),
					transport.MakeNodeQueue(),
					transport.MakeNodeType(),
					transport.MakeNodePortSet()));
		}

		#region ... details for ToNetworkEvent

		private static NodeQueue MakeNodeQueue(this NodeAddedEventTransport transport)
		{
			return new NodeQueue(ImmutableQueue.Create(transport.QueueContent.Select(x => new PacketIdentifier(x)).ToArray()), transport.QueueCapacity);
		}

		private static NodeType MakeNodeType(this NodeAddedEventTransport transport)
		{
			switch (transport.NodeType)
			{
				case (NODE_TYPE_GATEWAY): return new NodeType.Gateway();
				case (NODE_TYPE_ROUTER): return new NodeType.Router();
				case (NODE_TYPE_CONSUMER): return new NodeType.Consumer();
				default:
					throw new NotImplementedException();
			}
		}

		private static NodePortSet MakeNodePortSet(this NodeAddedEventTransport transport)
		{
			return new NodePortSet(new[]
			{
				MakeNodePort(transport.TopPortLinkIdentifier, transport.TopPortConnectionDirection),
				MakeNodePort(transport.BottomPortLinkIdentifier, transport.BottomPortConnectionDirection),
				MakeNodePort(transport.LeftPortLinkIdentifier, transport.LeftPortConnectionDirection),
				MakeNodePort(transport.RightPortLinkIdentifier, transport.RightPortConnectionDirection)
			});
		}

		private static NodePort MakeNodePort(Guid linkIdentifier, int connectionDirection)
		{
			return linkIdentifier == Guid.Empty
				? (NodePort)new NodePort.Connected(new LinkIdentifier(linkIdentifier), (ConnectionDirection)connectionDirection)
				: (NodePort)new NodePort.Disconnected();
		}

		#endregion
	}
}