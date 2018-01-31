using Photon;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PhotonNetworking.Photon
{

	public class PhotonRoomList : PunBehaviour, IRoomList
	{
		public IEnumerable<string> RoomList 
			=> PhotonNetwork.GetRoomList().Select(roomInfo => roomInfo.Name);

		public void CreateRoom(string name)
		{
			PhotonNetwork.CreateRoom(name);
		}

		public override void OnCreatedRoom()
		{
			Debug.Log("Room Created");
		}
	}
}