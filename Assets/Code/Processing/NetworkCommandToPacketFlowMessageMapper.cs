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
		private readonly Dictionary<int, Type> payloadTypeLookup = new Dictionary<int, Type>
		{
			{ TransportCommandPayloadType.AddNode, typeof(AddNodeCommandTransport) },
			{ TransportCommandPayloadType.LinkNodes, typeof(LinkNodesCommandTransport) },
			{ TransportCommandPayloadType.AddPacket, typeof(AddPacketCommandTransport) }
		};

		public PacketFlowMessage Map(int senderID, int senderType, NetworkCommand command)
		{
			return new PacketFlowMessage()
			{
				senderID = senderID,
				senderType = senderType,
				payloadType = command.Match(
					addNode => TransportCommandPayloadType.AddNode,
					linkNodes => TransportCommandPayloadType.LinkNodes,
					addPacket => TransportCommandPayloadType.AddPacket),
				payload = JsonUtility.ToJson(command.Match(
					addNodeCommand => GetPayloadForAddNodeCommand(addNodeCommand),
					linkNodesCommand => GetPayloadForLinkNodesCommand(linkNodesCommand),
					addPacketCommand => GetPayloadForAddPacketCommand(addPacketCommand)))
			};
		}
		
		private object GetPayloadForAddNodeCommand(NetworkCommand.AddNode addNodeCommand)
		{
			return new AddNodeCommandTransport
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
			return new LinkNodesCommandTransport
			{
				SourceID = linkNodesCommand.Source.Value,
				SourcePortDirection = (int)linkNodesCommand.SourcePort,
				SinkID = linkNodesCommand.Sink.Value,
				SinkPortDirection = (int)linkNodesCommand.SinkPort				
			};
		}		

		private object GetPayloadForAddPacketCommand(NetworkCommand.AddPacket addPacketCommand)
		{
			return new AddPacketCommandTransport
			{
				NodeId = addPacketCommand.NodeId.Value,
				Type = (int)addPacketCommand.Type,
				PacketID = addPacketCommand.PackedId.Value
			};
		}

		
		

		public NetworkCommand Map(PacketFlowMessage message)
		{
			return (NetworkCommand)JsonUtility.FromJson(message.payload, payloadTypeLookup[message.payloadType]);
		}			
	}
}
