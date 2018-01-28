using PacketFlow.Domain;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{
		private static object ToPacketConsumedEventTransport(this NetworkEvent.PacketConsumed source)
		{
			return new PacketConsumedEventTransport
			{
				PacketID = new SerializableGuid(source.PacketId.Value),
			};
		}

		private static NetworkEvent ToNetworkEvent(this PacketConsumedEventTransport transport)
		{
			return new NetworkEvent.PacketConsumed(
				new PacketIdentifier(transport.PacketID));
		}
	}
}