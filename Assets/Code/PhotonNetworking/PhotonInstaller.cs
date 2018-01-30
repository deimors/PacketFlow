using System;
using Zenject;

namespace PhotonNetworking
{
	public class PhotonInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<UseCases.ConnectAfterDelay>().AsSingle().WithArguments(TimeSpan.FromSeconds(1)).NonLazy();
		}
	}
}