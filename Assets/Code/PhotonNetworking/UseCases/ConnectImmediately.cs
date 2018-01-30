using UniRx;
using UnityEngine;

namespace PhotonNetworking.UseCases
{
	public class ConnectImmediately
	{
		public ConnectImmediately(IConnection connection)
		{
			connection.Subscribe(connectionEvent => Debug.Log(connectionEvent.GetType().Name));
			connection.Connect();
		}
	}
}
