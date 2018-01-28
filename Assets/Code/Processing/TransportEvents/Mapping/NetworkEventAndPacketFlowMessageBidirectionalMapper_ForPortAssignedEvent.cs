using PacketFlow.Domain;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{
		private static object ToPortAssignedEventTransport(this NetworkEvent.PortAssigned source)
		{
			return new PortAssignedEventTransport
			{
				NodeID = source.NodeId.Value,
				PortDirection = (int)source.Port,
				LinkID = source.LinkId.Value,
				ConnectionDirection = (int)source.Direction
			};
		}

		private static NetworkEvent ToNetworkEvent(this PortAssignedEventTransport transport)
		{
			return new NetworkEvent.PortAssigned(
				new NodeIdentifier(transport.NodeID),
				(PortDirection)transport.PortDirection,
				new LinkIdentifier(transport.LinkID),
				(ConnectionDirection)transport.ConnectionDirection);
		}
	}
}