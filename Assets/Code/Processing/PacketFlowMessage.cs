using UnityEngine.Networking;

namespace Assets.Code
{
	public class PacketFlowMessage : MessageBase
	{
		public int senderID;
		public int senderType;
		public string payload;		

		public PacketFlowMessage() { }
	}
}
