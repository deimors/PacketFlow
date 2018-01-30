using System;
using UniRx;
using UnityEngine;

namespace PhotonNetworking.UseCases
{
	public class ConnectAfterDelay
	{
		public ConnectAfterDelay(IConnection connection, TimeSpan delay)
		{
			connection.Subscribe(connectionEvent => Debug.Log(connectionEvent.GetType().Name));
			Observable.Timer(delay).Subscribe(_ => connection.Connect());
		}
	}
}
