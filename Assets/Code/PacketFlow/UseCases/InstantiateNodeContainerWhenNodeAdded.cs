using PacketFlow.Domain;
using PacketFlow.Presentation.Node;
using System;
using UniRx;

namespace PacketFlow.UseCases
{
	public class InstantiateNodeContainerWhenNodeAdded
	{
		public InstantiateNodeContainerWhenNodeAdded(IObservable<NetworkEvent> networkEvents, GatewayNodeContainer.Factory nodeFactory)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.NodeAdded>()
				.Subscribe(nodeAdded => CreateNodeContainer(nodeFactory, nodeAdded));
		}

		private static void CreateNodeContainer(GatewayNodeContainer.Factory nodeFactory, NetworkEvent.NodeAdded nodeAdded)
		{
			nodeFactory.Create(nodeAdded.Node.Id);
		}
	}
}