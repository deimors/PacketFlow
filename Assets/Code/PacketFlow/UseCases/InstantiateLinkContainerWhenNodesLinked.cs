using PacketFlow.Domain;
using PacketFlow.Presentation.Link;
using System;
using UniRx;

namespace PacketFlow.UseCases
{
	public class InstantiateLinkContainerWhenNodesLinked
	{
		public InstantiateLinkContainerWhenNodesLinked(IObservable<NetworkEvent> networkEvents, LinkContainer.Factory linkFactory)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.LinkAdded>()
				.Subscribe(linkAdded => linkFactory.Create(linkAdded.Link.Id));
		}
	}
}
