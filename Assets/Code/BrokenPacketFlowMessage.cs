using UnityEngine.Networking;

namespace Assets.Code
{
	public class BrokenPacketFlowMessage : MessageBase
	{
		public int SenderID { get; private set; }
		public int SenderType { get; private set; }
		public string Payload { get; private set; }

		public override void Serialize(NetworkWriter writer)
		{
			writer.Write(SenderID);
			writer.Write(SenderType);
			writer.Write(Payload);
		}

		public override void Deserialize(NetworkReader reader)
		{
			var m = reader.ReadMessage<BrokenPacketFlowMessage>();
			SenderID = m.SenderID;
			SenderType = m.SenderType;
			Payload = m.Payload;
		}

		public BrokenPacketFlowMessage() { }
		
		/// <summary>
		/// Make a packet flow message
		/// </summary>
		/// <param name="senderID">an arbitrary integer id in case we ever want to use multiple clients</param>
		/// <param name="senderType">whether this is an admin (1) or a hacker (-1)</param>
		/// <param name="payload">the message or command</param>
		public BrokenPacketFlowMessage(int senderID, int senderType, string payload)
		{
			SenderID = senderID;
			SenderType = senderType;
			Payload = payload;
		}
	}
}
