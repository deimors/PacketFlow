using UnityEngine;
using static Assets.Code.Constants;
using PacketFlow.Domain;
using static Assets.Code.Processing.TransportCommands;

namespace Assets.Code.Processing
{
	public class NetworkCommandToPacketFlowMessageMapper
	{
		public PacketFlowMessage Map(int senderID, int senderType, NetworkCommand command)
		{
			return new PacketFlowMessage()
			{
				senderID = senderID,
				senderType = senderType,
				payloadType = command.Match(
					addNode => (int)TransportCommandPayloadType.AddNode,
					linkNodes => (int)TransportCommandPayloadType.LinkNodes,
					addPacket => (int)TransportCommandPayloadType.AddPacket),
				payload = JsonUtility.ToJson(command.Match(
					addNodeCommand => GetPayloadForAddNodeCommand(addNodeCommand),
					linkNodesCommand => GetPayloadForLinkNodesCommand(linkNodesCommand),
					addPacketCommand => GetPayloadForAddPacketCommand(addPacketCommand)))
			};
		}
		
		private object GetPayloadForAddNodeCommand (NetworkCommand.AddNode addNodeCommand)
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
			switch ((TransportCommandPayloadType)message.payloadType)
			{
				case TransportCommandPayloadType.AddNode:
					return GetCommandForAddNodePayload(message.payload);
				case TransportCommandPayloadType.LinkNodes:
					return GetCommandForLinkNodesPayload(message.payload);
				case TransportCommandPayloadType.AddPacket:
					return GetCommandForAddPacketPayload(message.payload);
				default:
					return null;
			}
		}

		private NetworkCommand.AddNode GetCommandForAddNodePayload(string payload)
		{
			var transport = JsonUtility.FromJson<AddNodeCommandTransport>(payload);
			return new NetworkCommand.AddNode
				(
					new NodeIdentifier(transport.ID),
					new NodePosition(transport.X, transport.Y),
					transport.Capacity,
					new NodeType.Consumer()
				);
		}

		private NetworkCommand.LinkNodes GetCommandForLinkNodesPayload(string payload)
		{
			var transport = JsonUtility.FromJson<LinkNodesCommandTransport>(payload);
			return new NetworkCommand.LinkNodes
				(
					new NodeIdentifier(transport.SourceID),
					(PortDirection)transport.SourcePortDirection,
					new NodeIdentifier(transport.SinkID),
					(PortDirection)transport.SinkPortDirection
				);
		}

		private NetworkCommand.AddPacket GetCommandForAddPacketPayload(string payload)
		{
			var transport = JsonUtility.FromJson<AddPacketCommandTransport>(payload);
			return new NetworkCommand.AddPacket
				(
					new PacketIdentifier(transport.PacketID),
					(PacketType)transport.Type,
					new NodeIdentifier(transport.NodeId)
				);
		}
	}
}
