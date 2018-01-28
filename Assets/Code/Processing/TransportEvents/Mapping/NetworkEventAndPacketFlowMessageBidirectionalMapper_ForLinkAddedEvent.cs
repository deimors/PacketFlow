using PacketFlow.Domain;
using System.Collections.Immutable;
using System.Linq;

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
				SinkID = source.Link.Sink.Value,
				Bandwidth = source.Link.Attributes.Bandwidth,
				Latency = source.Link.Attributes.Latency,
				Content = source.Link.Content.Select(x => new SerializableGuid(x.Value)).ToList()
			};
		}

		private static NetworkEvent ToNetworkEvent(this LinkAddedEventTransport transport)
		{
			return new NetworkEvent.LinkAdded(
				new Link(
					new LinkIdentifier(transport.LinkID),
					new NodeIdentifier(transport.SourceID),
					new NodeIdentifier(transport.SinkID),
					new LinkAttributes(transport.Bandwidth, transport.Latency),
					ImmutableList.Create(transport.Content.Select(x => new PacketIdentifier(x)).ToArray())));
		}	
	}
}