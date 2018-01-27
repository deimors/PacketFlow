using PacketFlow.Domain;
using PacketFlow.UseCases;
using Zenject;

namespace PacketFlow.Presentation.Node
{
	public class NodeContainer : MonoInstaller
	{
		public class Factory : Factory<NodeIdentifier, NodeContainer> { }

		[Inject]
		public NodeIdentifier Identifier { get; set; }

		public override void InstallBindings()
		{
			Container.BindInstance(Identifier);

			Container.BindInstance(this);

			Container.Bind<SetNodePositionWhenNodeAdded>().AsSingle().NonLazy();
		}
	}
}
