using PacketFlow.Domain;
using PacketFlow.UseCases;
using Zenject;

namespace PacketFlow.Presentation.Packet
{
	public class PacketContainer : MonoInstaller
	{
		public class Factory : Factory<PacketIdentifier, PacketContainer> { }

		[Inject]
		public PacketIdentifier Identifier { get; set; }

		public override void InstallBindings()
		{
			Container.BindInstance(Identifier);
			Container.BindInstance(this);

			Container.Bind<SetPacketColourWhenPacketAdded>().AsSingle().NonLazy();
		}
	}
}
