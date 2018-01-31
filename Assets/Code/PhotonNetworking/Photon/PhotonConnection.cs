using Photon;
using System;
using UniRx;
using UnityEngine;

namespace PhotonNetworking.Photon
{
	public class PhotonConnection : PunBehaviour, IConnection
	{
		private readonly ISubject<ConnectionEvent> _connectionSubject = new Subject<ConnectionEvent>();

		public IObservable<ConnectionEvent> Events => _connectionSubject;

		void Start()
		{
			Send<ConnectionEvent.Disconnected>();
		}

		public void Connect()
		{
			if (PhotonNetwork.connectionState != ConnectionState.Disconnected)
				return;

			PhotonNetwork.ConnectUsingSettings("v0.1");
			Send<ConnectionEvent.Connecting>();
		}
		
		public void Disconnect()
		{
			if (PhotonNetwork.connectionState != ConnectionState.Connected)
				return;

			PhotonNetwork.Disconnect();
			Send<ConnectionEvent.Disconnecting>();
		}

		public override void OnConnectedToMaster()
		{
			Send<ConnectionEvent.Connected>();
		}

		public override void OnDisconnectedFromPhoton()
		{
			Send<ConnectionEvent.Disconnected>();
		}
		
		private void Send<TEvent>() where TEvent : ConnectionEvent, new()
		{
			_connectionSubject.OnNext(new TEvent());
		}
	}
}