using PacketFlow.Actors;
using PacketFlow.Domain;
using Zenject;

namespace PacketFlow.Presentation
{
	public class ClientInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.BindInterfacesTo<ActorClientProxy<NetworkEvent, NetworkCommand>>()
				.AsSingle();
		}
	}
}