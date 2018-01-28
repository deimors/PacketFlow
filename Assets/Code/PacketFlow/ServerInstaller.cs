using PacketFlow.Actors;
using PacketFlow.Domain;
using UniRx;
using Zenject;

namespace PacketFlow.Presentation
{
	public class ServerInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.BindInterfacesTo<ActorServerProxy<NetworkEvent, NetworkCommand>>()
				.AsSingle()
				.WithArguments<IActor<NetworkEvent, NetworkCommand>>(new NetworkActor(Observable.EveryUpdate().AsUnitObservable()));
		}
	}
}