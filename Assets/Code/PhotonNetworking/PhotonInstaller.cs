using System;
using Zenject;

namespace PhotonNetworking
{
	public class PhotonInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<UseCases.ConnectAfterDelay>().AsSingle().WithArguments(TimeSpan.FromSeconds(1)).NonLazy();
			//Container.Bind<UseCases.DisconnectAfterDelay>().AsSingle().WithArguments(TimeSpan.FromSeconds(5)).NonLazy();
		}
	}
}