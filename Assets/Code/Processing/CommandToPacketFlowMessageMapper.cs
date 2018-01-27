using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Processing
{
	public class ComnmandToPacketFlowMessageMapper
	{
		private const int SAMPLE_MESSAGE_TYPE_ID = -99;
		private readonly Dictionary<int, Type> payloadTypeLookup = new Dictionary<int, Type> { { SAMPLE_MESSAGE_TYPE_ID, typeof(int) } };

		public PacketFlowMessage Map(object thing)
		{
			return new PacketFlowMessage()
			{
				senderID = -99,
				senderType = -99,
				payloadType = SAMPLE_MESSAGE_TYPE_ID,
				payload = JsonUtility.ToJson(thing)
			};
		}

		public object Map(PacketFlowMessage message)
		{
			return JsonUtility.FromJson(message.payload, payloadTypeLookup[message.payloadType]);
		}

			
	}
}
