using System.Collections.Generic;

namespace PhotonNetworking.Photon
{
	public interface IRoomList
	{
		IEnumerable<string> RoomList { get; }

		void CreateRoom(string name);
	}
}