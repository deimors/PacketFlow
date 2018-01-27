using PacketFlow.Domain;
using PacketFlow.Presentation.Node;
using System;
using UniRx;

namespace PacketFlow.UseCases
{
	public class InstantiateNodeContainerWhenNodeAdded
	{
		private readonly GatewayNodeContainer.Factory gatewayNodeFactory;
		private readonly RouterNodeContainer.Factory routerNodeFactory;
		private readonly ConsumerNodeContainer.Factory consumerNodeFactory;

		public InstantiateNodeContainerWhenNodeAdded(IObservable<NetworkEvent> networkEvents, GatewayNodeContainer.Factory gatewayNodeFactory, RouterNodeContainer.Factory routerNodeFactory, ConsumerNodeContainer.Factory consumerNodeFactory)
		{
			this.gatewayNodeFactory = gatewayNodeFactory;
			this.routerNodeFactory = routerNodeFactory;
			this.consumerNodeFactory = consumerNodeFactory;

			networkEvents
				.OfType<NetworkEvent, NetworkEvent.NodeAdded>()
				.Subscribe(nodeAdded => CreateNodeContainer(nodeAdded));
		}

		private void CreateNodeContainer(NetworkEvent.NodeAdded nodeAdded)
		{
			nodeAdded.Node.Switch(
				gateway => gatewayNodeFactory.Create(nodeAdded.Node.Id),
				router => routerNodeFactory.Create(nodeAdded.Node.Id),
				consumer => consumerNodeFactory.Create(nodeAdded.Node.Id)
			);
		}
	}
}