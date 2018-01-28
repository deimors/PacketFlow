using PacketFlow.Domain;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{
		private static object ToPacketTransmissionStartedEventTransport(this NetworkEvent.PacketTransmissionStarted source)
		{
			return new PacketTransmissionStartedEventTransport
			{
				PacketID = new SerializableGuid(source.PacketId.Value),
				LinkID = new SerializableGuid(source.LinkId.Value)
			};
		}

		private static NetworkEvent ToNetworkEvent(this PacketTransmissionStartedEventTransport transport)
		{
			return new NetworkEvent.PacketTransmissionStarted(
				new PacketIdentifier(transport.PacketID),
				new LinkIdentifier(transport.LinkID));
		}
	}
}