using PacketFlow.Domain;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{
		private static object ToPacketEnqueuedEventTransport(this NetworkEvent.PacketEnqueued source)
		{
			return new PacketEnqueuedEventTransport
			{
				NodeID = source.NodeId.Value,
				PacketID = source.PacketId.Value
			};
		}

		private static NetworkEvent ToNetworkEvent(this PacketEnqueuedEventTransport transport)
		{
			return new NetworkEvent.PacketEnqueued(
				new PacketIdentifier(transport.PacketID),
				new NodeIdentifier(transport.NodeID));
		}
	}
}