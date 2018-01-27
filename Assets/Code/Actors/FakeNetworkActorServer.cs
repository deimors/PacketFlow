using System;
using UniRx;
using PacketFlow.Domain;
using UnityEngine;

namespace PacketFlow.Actors
{
	public class FakeNetworkActorServer : IActorServer<NetworkEvent, NetworkCommand>
	{
		public IObservable<NetworkCommand> ReceivedCommands { get { return Observable.Empty<NetworkCommand>(); } }

		public void SendEvent(NetworkEvent @event)
		{
			Debug.Log($"Sending event {@event.GetType().Name}");
		}
	}
}
