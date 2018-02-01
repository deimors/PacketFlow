using System;
using UniRx;

namespace MultiNet.UseCases
{
	public class DisconnectAfterDelay
	{
		public DisconnectAfterDelay(IConnection connection, TimeSpan delay)
		{
			Observable.Timer(delay).Subscribe(_ => connection.Disconnect());
		}
	}
}
