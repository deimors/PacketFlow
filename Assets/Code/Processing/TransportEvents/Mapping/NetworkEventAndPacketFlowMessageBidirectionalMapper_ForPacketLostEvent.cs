using PacketFlow.Domain;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{
		private static object ToPacketLostEventTransport(this NetworkEvent.PacketLost source)
		{
			return new PacketLostEventTransport
			{
				PacketID = new SerializableGuid(source.PacketId.Value),
			};
		}

		private static NetworkEvent ToNetworkEvent(this PacketLostEventTransport transport)
		{
			return new NetworkEvent.PacketLost(
				new PacketIdentifier(transport.PacketID));
		}
	}
}