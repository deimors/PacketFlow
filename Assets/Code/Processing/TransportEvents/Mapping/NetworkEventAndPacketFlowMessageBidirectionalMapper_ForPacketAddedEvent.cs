using PacketFlow.Domain;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{
		private static object ToPacketAddedEventTransport(this NetworkEvent.PacketAdded source)
		{
			return new PacketAddedEventTransport
			{
				PacketID = new SerializableGuid(source.Packet.Id.Value),
				PacketType = (int)source.Packet.Type
			};
		}

		private static NetworkEvent ToNetworkEvent(this PacketAddedEventTransport transport)
		{
			return new NetworkEvent.PacketAdded(new Packet(
				new PacketIdentifier(transport.PacketID),
				(PacketType)transport.PacketType));
		}
	}
}