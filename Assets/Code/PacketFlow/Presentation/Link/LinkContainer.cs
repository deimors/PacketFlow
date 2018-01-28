using PacketFlow.Domain;
using PacketFlow.UseCases;
using Zenject;

namespace PacketFlow.Presentation.Link
{
	public class LinkContainer : MonoInstaller
	{
		public class Factory : Factory<LinkIdentifier, LinkContainer> { }

		[Inject]
		public LinkIdentifier Identifier { get; set; }

		public override void InstallBindings()
		{
			Container.BindInstance(Identifier);
			Container.BindInstance(this);

			Container.Bind<SetNetworkLinkEndsWhenNodesLinked>().AsSingle().NonLazy();
		}
	}
}
