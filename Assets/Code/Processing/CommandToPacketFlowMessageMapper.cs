using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Code.Constants;

namespace Assets.Code.Processing
{
	public class CommandToPacketFlowMessageMapper<NetworkCommand>
	{
		private readonly Dictionary<int, Type> payloadTypeLookup = new Dictionary<int, Type> { { COMMAND_PAYLOAD_TYPE, typeof(NetworkCommand) } };

		public PacketFlowMessage Map(int senderID, int senderType, NetworkCommand command)
		{
			return new PacketFlowMessage()
			{
				senderID = senderID,
				senderType = senderType,
				payloadType = COMMAND_PAYLOAD_TYPE,
				payload = JsonUtility.ToJson(command)
			};
		}

		public NetworkCommand Map(PacketFlowMessage message)
		{
			return (NetworkCommand)JsonUtility.FromJson(message.payload, payloadTypeLookup[message.payloadType]);
		}			
	}
}
