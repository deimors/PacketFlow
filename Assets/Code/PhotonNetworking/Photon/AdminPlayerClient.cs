using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace PhotonNetworking.Photon
{
	public class AdminPlayerClient : MonoBehaviour, IActorServer<NetworkEvent, NetworkCommand>
	{
		private PhotonView photonView;

		private readonly ISubject<NetworkCommand> _commandSubject = new Subject<NetworkCommand>();
		public IObservable<NetworkCommand> ReceivedCommands => _commandSubject;

		// Use this for initialization
		void Start()
		{
			photonView = PhotonView.Get(this);
		}

		public void SendEvent(NetworkEvent @event)
		{
			
		}
		
	}
}