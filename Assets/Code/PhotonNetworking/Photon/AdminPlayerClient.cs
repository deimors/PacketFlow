using PacketFlow.Actors;
using PacketFlow.Domain;
using Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace PhotonNetworking.Photon
{
	public class AdminPlayerClient : PunBehaviour, IActorHost<NetworkEvent, NetworkCommand>
	{
		private readonly ISubject<NetworkCommand> _commandSubject = new Subject<NetworkCommand>();
		public IObservable<NetworkCommand> ReceivedCommands => _commandSubject;
		
		public void SendEvent(NetworkEvent @event)
		{
			
		}
		
	}
}