using PacketFlow.Domain;
using System;
using UniRx;

namespace PacketFlow.UseCases
{
	public class SetNetworkLinkEndsWhenNodesLinked
	{
		public SetNetworkLinkEndsWhenNodesLinked(LinkIdentifier linkId, IObservable<NetworkEvent> networkEvents, IDisplayLink displayLink, NodePositionReadModel nodePositions)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.LinkAdded>()
				.Where(linkAdded => linkAdded.Link.Id == linkId)
				.Subscribe(linkAdded => displayLink.DisplayLink(nodePositions[linkAdded.Link.Source], nodePositions[linkAdded.Link.Sink]));
		}
	}
}