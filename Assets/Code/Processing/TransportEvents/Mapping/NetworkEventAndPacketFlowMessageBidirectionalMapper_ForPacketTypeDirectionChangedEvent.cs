using PacketFlow.Domain;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{		
		private static object ToPacketTypeDirectionChanged(this NetworkEvent.PacketTypeDirectionChanged source)
		{
			return new PacketTypeDirectionChangedEventTransport
			{
				NodeID = new SerializableGuid(source.NodeId.Value),
				PacketType = (int)source.PacketType,
				PortDirection = (int)source.Port
			};
		}

		private static NetworkEvent ToNetworkEvent(this PacketTypeDirectionChangedEventTransport transport)
		{
			return new NetworkEvent.PacketTypeDirectionChanged(
				new NodeIdentifier(transport.NodeID),
				(PacketType)transport.PacketType,
				(PortDirection)transport.PortDirection);
		}
	}
}