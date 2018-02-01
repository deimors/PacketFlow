using System.Collections.Generic;

namespace MultiNet.Photon
{

	public interface IRoomList
	{
		IEnumerable<string> RoomList { get; }

		void CreateRoom(string name);
	}
}