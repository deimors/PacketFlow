using PhotonNetworking.Photon;
using System;
using Zenject;

namespace PhotonNetworking
{
	public class PhotonInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<UseCases.ConnectAfterDelay>().AsSingle().WithArguments(TimeSpan.FromSeconds(1)).NonLazy();
			//Container.Bind<UseCases.GetRoomListOnConnected>().AsSingle().NonLazy();
			Container.Bind<UseCases.CreateRoomAfterDelay>().AsSingle().WithArguments("packetflow", TimeSpan.FromSeconds(1)).NonLazy();
		}
	}
}