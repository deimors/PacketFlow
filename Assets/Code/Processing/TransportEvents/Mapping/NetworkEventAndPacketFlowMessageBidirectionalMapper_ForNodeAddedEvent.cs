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
				NodeID = new SerializableGuid(source.Node.Id.Value),
				PositionX = source.Node.Position.X,
				PositionY = source.Node.Position.Y,
				QueueContent = source.Node.Queue.Content.Select(x => new SerializableGuid(x.Value)).ToList(),
				QueueCapacity = source.Node.Queue.Capacity,
				TopPortLinkIdentifier = new SerializableGuid(source.Node.Ports.GetLinkIdentifier(PortDirection.Top)),
				TopPortConnectionDirection = source.Node.Ports.GetLinkDirection(PortDirection.Top),
				BottomPortLinkIdentifier = new SerializableGuid(source.Node.Ports.GetLinkIdentifier(PortDirection.Bottom)),
				BottomPortConnectionDirection = source.Node.Ports.GetLinkDirection(PortDirection.Bottom),
				LeftPortLinkIdentifier = new SerializableGuid(source.Node.Ports.GetLinkIdentifier(PortDirection.Left)),
				LeftPortConnectionDirection = source.Node.Ports.GetLinkDirection(PortDirection.Left),
				RightPortLinkIdentifier = new SerializableGuid(source.Node.Ports.GetLinkIdentifier(PortDirection.Right)),
				RightPortConnectionDirection = source.Node.Ports.GetLinkDirection(PortDirection.Right),
				RouterState = source.Node.Match(
					gateway => Enumerable.Empty<int>().ToArray(),
					router => new[] { router.State[PacketType.Red], router.State[PacketType.Blue], router.State[PacketType.Green] }.Select(x => (int)x).ToArray(),
					consumer => Enumerable.Empty<int>().ToArray())
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
			switch (transport.NodeType)
			{
				case NODE_TYPE_GATEWAY:
					return new NetworkEvent.NodeAdded(new Node.Gateway(
							new NodeIdentifier(transport.NodeID),
							new NodePosition(transport.PositionX, transport.PositionY),
							transport.MakeNodeQueue(),
							transport.MakeNodePortSet()));
				case NODE_TYPE_ROUTER:
					return new NetworkEvent.NodeAdded(new Node.Router(
							new NodeIdentifier(transport.NodeID),
							new NodePosition(transport.PositionX, transport.PositionY),
							transport.MakeNodeQueue(),
							transport.MakeNodePortSet(),
							transport.MakeRouterState()));
				case NODE_TYPE_CONSUMER:
					return new NetworkEvent.NodeAdded(new Node.Consumer(
							new NodeIdentifier(transport.NodeID),
							new NodePosition(transport.PositionX, transport.PositionY),
							transport.MakeNodeQueue(),
							transport.MakeNodePortSet()));
				default: throw new Exception("NODE TYPE DOES'T EXIST");
			}
		}

		#region ... details for ToNetworkEvent

		private static NodeQueue MakeNodeQueue(this NodeAddedEventTransport transport)
		{
			return new NodeQueue(ImmutableQueue.Create(transport.QueueContent.Select(x => new PacketIdentifier(x)).ToArray()), transport.QueueCapacity);
		}
		
		private static NodePortSet MakeNodePortSet(this NodeAddedEventTransport transport)
		{
			return new NodePortSet(new[]
			{
				MakeNodePort(transport.TopPortDirection, transport.TopPortLinkIdentifier, transport.TopPortConnectionDirection),
				MakeNodePort(transport.BottomPortDirection, transport.BottomPortLinkIdentifier, transport.BottomPortConnectionDirection),
				MakeNodePort(transport.LeftPortDirection, transport.LeftPortLinkIdentifier, transport.LeftPortConnectionDirection),
				MakeNodePort(transport.RightPortDirection, transport.RightPortLinkIdentifier, transport.RightPortConnectionDirection)
			});
		}

		private static NodePort MakeNodePort(int portDirection, Guid linkIdentifier, int connectionDirection)
		{
			return linkIdentifier == Guid.Empty
				? (NodePort)new NodePort.Connected((PortDirection)portDirection, new LinkIdentifier(linkIdentifier), (ConnectionDirection)connectionDirection)
				: (NodePort)new NodePort.Disconnected((PortDirection)portDirection);
		}

		private static RouterState MakeRouterState(this NodeAddedEventTransport transport)
		{
			return new RouterState(transport.RouterState.Select(x => (PortDirection)x).ToArray());
		}

		#endregion
	}
}