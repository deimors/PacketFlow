using PacketFlow.Actors;
using UniRx;
using Zenject;

namespace PacketFlow.Presentation
{
	public class SingleActorInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.BindInterfacesTo<NetworkActor>()
				.AsSingle()
				.WithArguments(Observable.EveryUpdate().AsUnitObservable());
		}
	}
}