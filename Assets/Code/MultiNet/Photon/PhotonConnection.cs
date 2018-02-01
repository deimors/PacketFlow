using Photon;
using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace MultiNet.Photon
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

			PhotonNetwork.autoJoinLobby = true;

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

		public override void OnConnectionFail(DisconnectCause cause)
		{
			Debug.Log($"Connection Failed: {cause.ToString()}");
			Send<ConnectionEvent.Disconnected>();
		}

		public override void OnConnectedToPhoton()
		{
			Debug.Log("OnConnectedToPhoton()");
			Send<ConnectionEvent.Connected>();
		}

		public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
		{
			Debug.Log($"OnPhotonPlayerConnected({newPlayer.ToString()})");
		}

		public override void OnFailedToConnectToPhoton(DisconnectCause cause)
		{
			Debug.Log($"OnFailedToConnectToPhoton({cause.ToString()}");
		}

		public override void OnJoinedLobby()
		{
			Debug.Log($"OnJoinedLobby({PhotonNetwork.lobby.ToString()})");
			Debug.Log($"Rooms: {string.Join(", ", PhotonNetwork.GetRoomList().Select(room => room.ToString()))}");
		}

		public override void OnCreatedRoom()
		{
			Debug.Log("OnCreatedRoom()");
			Debug.Log($"Rooms: {string.Join(", ", PhotonNetwork.GetRoomList().Select(room => room.ToString()))}");
		}

		public override void OnJoinedRoom()
		{
			Debug.Log($"OnJoinedRoom({PhotonNetwork.room.ToString()})");
		}

		public override void OnReceivedRoomListUpdate()
		{
			Debug.Log($"OnReceivedRoomListUpdate({string.Join(", ", PhotonNetwork.GetRoomList().Select(room => room.ToString()))})");
		}

		public override void OnConnectedToMaster()
		{
			Debug.Log("OnConnectedToMaster()");
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