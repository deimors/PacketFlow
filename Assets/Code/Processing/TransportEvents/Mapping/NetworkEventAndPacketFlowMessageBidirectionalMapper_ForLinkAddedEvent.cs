using PacketFlow.Domain;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{
		private static object ToLinkAddedEventTransport(this NetworkEvent.LinkAdded source)
		{
			return new LinkAddedEventTransport()
			{
				LinkID = source.Link.Id.Value,
				SourceID = source.Link.Source.Value,
				SinkID = source.Link.Sink.Value
			};
		}

		private static NetworkEvent ToNetworkEvent(this LinkAddedEventTransport transport)
		{
			return new NetworkEvent.LinkAdded(
				new Link(
					new LinkIdentifier(transport.LinkID), 
					new NodeIdentifier(transport.SourceID), 
					new NodeIdentifier(transport.SinkID)));
		}
	}
}