using PacketFlow.Actors;
using PacketFlow.Domain;
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
		private GameObject _nodePrefab;

		public override void InstallBindings()
		{
			Container.BindFactory<NodeIdentifier, GatewayNodeContainer, GatewayNodeContainer.Factory>()
				.FromSubContainerResolve()
				.ByNewPrefab<GatewayNodeContainer>(_nodePrefab);
			
			
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
			Container.Bind<CreateNodeAfterDelay>().AsSingle().NonLazy();
			Container.Bind<InstantiateNodeContainerWhenNodeAdded>().AsSingle().NonLazy();
		}
	}
}