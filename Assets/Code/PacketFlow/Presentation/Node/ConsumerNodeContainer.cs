using PacketFlow.Domain;
using PacketFlow.UseCases;
using System;
using Zenject;

namespace PacketFlow.Presentation.Node
{
	public class ConsumerNodeContainer : MonoInstaller
	{
		public class Factory : Factory<NodeIdentifier, ConsumerNodeContainer> { }

		[Inject]
		public NodeIdentifier Identifier { get; set; }

		public override void InstallBindings()
		{
			Container.BindInstance(Identifier);

			Container.BindInstance(this);

			Container.Bind<SetNodePositionWhenNodeAdded>().AsSingle().NonLazy();
			Container.Bind<ProcessNodeQueuePeriodically>().AsSingle().WithArguments(TimeSpan.FromSeconds(5f)).NonLazy();

			Container.Bind<DisplayOnPacketEnqueued>().AsSingle().NonLazy();
			Container.Bind<DisplayOnPacketDequeued>().AsSingle().NonLazy();
		}
	}
}
