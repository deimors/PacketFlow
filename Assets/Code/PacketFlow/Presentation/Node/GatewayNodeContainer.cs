using PacketFlow.Domain;
using PacketFlow.UseCases;
using System;
using Assets.Code.Queue;
using Zenject;

namespace PacketFlow.Presentation.Node
{
	public class GatewayNodeContainer : MonoInstaller
	{
		public class Factory : Factory<NodeIdentifier, GatewayNodeContainer> { }

		[Inject]
		public NodeIdentifier Identifier { get; set; }

		public override void InstallBindings()
		{
			Container.BindInstance(Identifier);

			Container.BindInstance(this);

			Container.Bind<SetNodePositionWhenNodeAdded>().AsSingle().NonLazy();
			Container.Bind<ProcessNodeQueuePeriodically>().AsSingle().WithArguments(TimeSpan.FromSeconds(.5f)).NonLazy();
			//Container.Bind<AddPacketsPeriodically>().AsSingle().NonLazy();
		}
	}
}
