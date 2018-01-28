using PacketFlow.Domain;
using PacketFlow.Presentation.Link;
using PacketFlow.Presentation.Node;
using PacketFlow.Presentation.Packet;
using PacketFlow.UseCases;
using UnityEngine;
using Zenject;

namespace PacketFlow.Presentation
{
	public class PacketFlowInstaller : MonoInstaller
	{
		[SerializeField]
		private GameObject _gatewayNodePrefab;

		[SerializeField]
		private GameObject _routerNodePrefab;

		[SerializeField]
		private GameObject _consumerNodePrefab;

		[SerializeField]
		private GameObject _linkPrefab;

		[SerializeField]
		private GameObject _packetPrefab;

		public override void InstallBindings()
		{
			Container.BindFactory<NodeIdentifier, GatewayNodeContainer, GatewayNodeContainer.Factory>()
				.FromSubContainerResolve()
				.ByNewPrefab<GatewayNodeContainer>(_gatewayNodePrefab);

			Container.BindFactory<NodeIdentifier, RouterNodeContainer, RouterNodeContainer.Factory>()
				.FromSubContainerResolve()
				.ByNewPrefab<RouterNodeContainer>(_routerNodePrefab);

			Container.BindFactory<NodeIdentifier, ConsumerNodeContainer, ConsumerNodeContainer.Factory>()
				.FromSubContainerResolve()
				.ByNewPrefab<ConsumerNodeContainer>(_consumerNodePrefab);

			Container.BindFactory<LinkIdentifier, LinkContainer, LinkContainer.Factory>()
				.FromSubContainerResolve()
				.ByNewPrefab<LinkContainer>(_linkPrefab);

			Container.BindFactory<PacketIdentifier, PacketContainer, PacketContainer.Factory>()
				.FromSubContainerResolve()
				.ByNewPrefab<PacketContainer>(_packetPrefab);

			/*
			Container
				.BindInterfacesTo<NetworkActor>()
				.AsSingle()
				.WithArguments(Observable.EveryUpdate().AsUnitObservable());
			*/
			Container.Bind<NodePositionReadModel>().AsSingle().NonLazy();
			Container.Bind<LinkLatencyReadModel>().AsSingle().NonLazy();
			Container.Bind<PacketTypeReadModel>().AsSingle().NonLazy();

			Container.Bind<BuildLevel>().AsSingle().NonLazy();
			Container.Bind<InstantiateNodeContainerWhenNodeAdded>().AsSingle().NonLazy();
			Container.Bind<InstantiateLinkContainerWhenNodesLinked>().AsSingle().NonLazy();
		}
	}
}