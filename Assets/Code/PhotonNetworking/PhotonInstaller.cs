using Zenject;

namespace PhotonNetworking
{
	public class PhotonInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<UseCases.ConnectImmediately>().AsSingle().NonLazy();
		}
	}
}