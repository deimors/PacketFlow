using Photon;
using System;
using UniRx;
using UnityEngine;

namespace PhotonNetworking.Photon
{
	public class PhotonConnection : PunBehaviour, IConnection
	{
		private readonly ISubject<ConnectionEvent> _connectionSubject = new Subject<ConnectionEvent>();

		void Start()
		{
			Send<ConnectionEvent.Disconnected>();
		}

		public void Connect()
		{
			PhotonNetwork.ConnectUsingSettings("v0.1");
			Send<ConnectionEvent.Connecting>();
		}
		
		public void Disconnect()
		{
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

		public IDisposable Subscribe(IObserver<ConnectionEvent> observer)
			=> _connectionSubject.Subscribe(observer);

		private void Send<TEvent>() where TEvent : ConnectionEvent, new()
		{
			_connectionSubject.OnNext(new TEvent());
		}
	}
}