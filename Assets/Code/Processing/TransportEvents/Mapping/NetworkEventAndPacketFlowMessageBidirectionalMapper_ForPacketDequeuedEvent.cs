using PacketFlow.Domain;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{
		private static object ToPacketDequeuedEventTransport(this NetworkEvent.PacketDequeued source)
		{
			return new PacketDequeuedEventTransport
			{
				NodeID = new SerializableGuid(source.NodeId.Value),
			};
		}

		private static NetworkEvent ToNetworkEvent(this PacketDequeuedEventTransport transport)
		{
			return new NetworkEvent.PacketDequeued(
				new NodeIdentifier(transport.NodeID));
		}
	}
}