using PacketFlow.Domain;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{
		private static object ToLinkAddedEventTransport(this NetworkEvent.LinkAdded source)
		{
			return new LinkAddedEventTransport();
		}

		#region ... details for ToLinkAddedEventTransport

		#endregion

		private static NetworkEvent ToNetworkEvent(this LinkAddedEventTransport transport)
		{
			return null;
		}

		#region ... details for ToNetworkEvent

		#endregion
	}
}