using PacketFlow.Actors;
using PacketFlow.Domain;
using PacketFlow.Presentation.Link;
using PacketFlow.Presentation.Node;
using PacketFlow.UseCases;
using UniRx;
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

			Container
				.BindInterfacesTo<ActorServerProxy<NetworkEvent, NetworkCommand>>()
				.FromInstance(new ActorServerProxy<NetworkEvent, NetworkCommand>(new NetworkActor(Observable.EveryUpdate().AsUnitObservable()), new FakeNetworkActorServer()))
				.AsSingle();
			/*
			Container
				.BindInterfacesTo<NetworkActor>()
				.AsSingle()
				.WithArguments(Observable.EveryUpdate().AsUnitObservable());
			*/
			Container.Bind<NodePositionReadModel>().AsSingle().NonLazy();

			Container.Bind<CreateNodeAfterDelay>().AsSingle().NonLazy();
			Container.Bind<InstantiateNodeContainerWhenNodeAdded>().AsSingle().NonLazy();
			Container.Bind<InstantiateLinkContainerWhenNodesLinked>().AsSingle().NonLazy();
		}
	}
}