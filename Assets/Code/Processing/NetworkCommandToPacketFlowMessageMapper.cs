using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Code.Constants;
using PacketFlow.Domain;
using static Assets.Code.Processing.TransportCommands;

namespace Assets.Code.Processing
{
	public class NetworkCommandToPacketFlowMessageMapper
	{
		private readonly Dictionary<int, Type> payloadTypeLookup = new Dictionary<int, Type> { { COMMAND_PAYLOAD_TYPE, typeof(NetworkCommand) } };

		public PacketFlowMessage Map(int senderID, int senderType, NetworkCommand command)
		{
			return new PacketFlowMessage()
			{
				senderID = senderID,
				senderType = senderType,
				payloadType = COMMAND_PAYLOAD_TYPE,
				payload = JsonUtility.ToJson(command.Match(
					addNodeCommand => GetPayloadForAddNodeCommand(addNodeCommand),
					linkNodesCommand => GetPayloadForLinkNodesCommand(linkNodesCommand),
					addPacketCommand => GetPayloadForAddPacketCommand(addPacketCommand)))
			};
		}
		
		private object GetPayloadForAddNodeCommand(NetworkCommand.AddNode addNodeCommand)
		{
			return new AddNodeCommand
			{
				ID = addNodeCommand.NodeId.Value,
				X = addNodeCommand.Position.X,
				Y = addNodeCommand.Position.Y,
				Capacity = addNodeCommand.Capacity,
				NodeType = addNodeCommand.Type.Match(
						gateway => TransportNodeType.Gateway,
						router => TransportNodeType.Router,
						consumer => TransportNodeType.Consumer)
			};
		}

		

		private object GetPayloadForLinkNodesCommand(NetworkCommand.LinkNodes linkNodesCommand)
		{
			return new LinkNodeCommand
			{
				

			};
		}

		

		private object GetPayloadForAddPacketCommand(NetworkCommand.AddPacket addPacketCommand)
		{
			return null;
		}

		
		

		public NetworkCommand Map(PacketFlowMessage message)
		{
			return (NetworkCommand)JsonUtility.FromJson(message.payload, payloadTypeLookup[message.payloadType]);
		}			
	}
}
