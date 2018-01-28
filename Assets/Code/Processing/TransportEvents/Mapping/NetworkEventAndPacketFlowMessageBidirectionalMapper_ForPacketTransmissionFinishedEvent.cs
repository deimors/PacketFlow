using PacketFlow.Domain;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{
		private static object ToPackedTransmissionFinishedEventTransport(this NetworkEvent.PacketTransmissionFinished source)
		{
			return new PacketTransmissionFinishedEventTransport
			{
				PacketID = new SerializableGuid(source.PacketId.Value),
				LinkID = new SerializableGuid(source.LinkId.Value)
			};
		}

		private static NetworkEvent ToNetworkEvent(this PacketTransmissionFinishedEventTransport transport)
		{
			return new NetworkEvent.PacketTransmissionFinished(
				new PacketIdentifier(transport.PacketID),
				new LinkIdentifier(transport.LinkID));
		}
	}
}