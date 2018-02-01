using MultiNet.Photon;
using System;
using UniRx;
using UnityEngine;

namespace MultiNet.UseCases
{
	public class ConnectAfterDelay
	{
		public ConnectAfterDelay(IConnection connection, TimeSpan delay)
		{
			connection.Events.Subscribe(connectionEvent => Debug.Log(connectionEvent.GetType().Name));
			Observable.Timer(delay).Subscribe(_ => connection.Connect());
		}
	}

	public class GetRoomListOnConnected
	{
		public GetRoomListOnConnected(IConnection connection, IRoomList rooms)
		{
			//connection.Events.OfType<ConnectionEvent, ConnectionEvent.Connected>()
			Observable.Interval(TimeSpan.FromSeconds(1))
				.Subscribe(_ => Debug.Log($"rooms: {string.Join(", ", rooms.RoomList)}"));
		}
	}

	public class CreateRoomAfterDelay
	{
		public CreateRoomAfterDelay(IConnection connection, IRoomList rooms, string roomName, TimeSpan delay)
		{
			connection.Events
				.OfType<ConnectionEvent, ConnectionEvent.Connected>()
				.Delay(delay)
				.Subscribe(_ => CreateRoom(rooms, roomName));
		}

		private static void CreateRoom(IRoomList rooms, string roomName)
		{
			rooms.CreateRoom(roomName);
		}
	}
}
