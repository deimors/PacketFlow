using System;
using UniRx;

namespace PhotonNetworking.UseCases
{
	public class DisconnectAfterDelay
	{
		public DisconnectAfterDelay(IConnection connection, TimeSpan delay)
		{
			Observable.Timer(delay).Subscribe(_ => connection.Disconnect());
		}
	}
}
