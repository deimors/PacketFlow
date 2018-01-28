using UnityEngine;
using static Assets.Code.Constants;
using PacketFlow.Domain;
using static Assets.Code.Processing.TransportCommands;

namespace Assets.Code.Processing
{
	public class NetworkCommandAndPacketFlowMessageBidirectionalMapper
	{
		public PacketFlowMessage Map(int senderID, int senderType, NetworkCommand command)
		{
			return new PacketFlowMessage()
			{
				senderID = senderID,
				senderType = senderType,
				payloadType = command.Match(
					agn => (int)TransportCommandPayloadType.AddGatewayNode,
					arn => (int)TransportCommandPayloadType.AddRouterNode,
					acn => (int)TransportCommandPayloadType.AddConsumerNode,
					ln => (int)TransportCommandPayloadType.LinkNodes,
					ap => (int)TransportCommandPayloadType.AddPacket,
					iptd => (int)TransportCommandPayloadType.IncrementPacketTypeDirection,
					pnq => (int)TransportCommandPayloadType.ProcessNodeQueue),
				payload = JsonUtility.ToJson(command.Match(
					agn => GetPayloadForAddGatewayNodeCommand(agn),
					arn => GetPayloadForAddRouterNodeCommand(arn),
					acn => GetPayloadForAddConsumerNodeCommand(acn),
					ln => GetPayloadForLinkNodesCommand(ln),
					ap => GetPayloadForAddPacketCommand(ap),
					iptd => GetPayloadForIncrementPacketTypeDirectionCommand(iptd),
					pnq => GetPayloadForProcessNodeQueueCommand(pnq)))
			};
		}

		#region Get Payloads for Commands
		private object GetPayloadForAddGatewayNodeCommand (NetworkCommand.AddGatewayNode addGatewayNode)
		{
			return new AddGatewayNodeCommandTransport
			{
				NodeID = addGatewayNode.NodeId.Value,
				X = addGatewayNode.Position.X,
				Y = addGatewayNode.Position.Y,
				Capacity = addGatewayNode.Capacity
			};
		}

		private object GetPayloadForAddRouterNodeCommand(NetworkCommand.AddRouterNode addRouterNode)
		{
			return new AddRouterNodeCommandTransport
			{
				NodeID = addRouterNode.NodeId.Value,
				X = addRouterNode.Position.X,
				Y = addRouterNode.Position.Y,
				Capacity = addRouterNode.Capacity
			};
		}

		private object GetPayloadForAddConsumerNodeCommand(NetworkCommand.AddConsumerNode addConsumerNode)
		{
			return new AddConsumerNodeCommandTransport
			{
				NodeID = addConsumerNode.NodeId.Value,
				X = addConsumerNode.Position.X,
				Y = addConsumerNode.Position.Y,
				Capacity = addConsumerNode.Capacity
			};
		}

		private object GetPayloadForLinkNodesCommand(NetworkCommand.LinkNodes linkNodesCommand)
		{
			return new LinkNodesCommandTransport
			{
				SourceID = linkNodesCommand.Source.Value,
				SourcePortDirection = (int)linkNodesCommand.SourcePort,
				SinkID = linkNodesCommand.Sink.Value,
				SinkPortDirection = (int)linkNodesCommand.SinkPort,
				Bandwidth = linkNodesCommand.Attributes.Bandwidth,
				Latency = linkNodesCommand.Attributes.Latency
			};
		}		

		private object GetPayloadForAddPacketCommand(NetworkCommand.AddPacket addPacketCommand)
		{
			return new AddPacketCommandTransport
			{
				NodeID = addPacketCommand.NodeId.Value,
				Type = (int)addPacketCommand.Type,
				PacketID = addPacketCommand.PackedId.Value
			};
		}

		private object GetPayloadForIncrementPacketTypeDirectionCommand(NetworkCommand.IncrementPacketTypeDirection incrementPacketTypeDirection)
		{
			return new IncrementPacketTypeDirectionCommandTransport
			{
				NodeID = incrementPacketTypeDirection.NodeId.Value,
				Type = (int)incrementPacketTypeDirection.PacketType
			};
		}

		private object GetPayloadForProcessNodeQueueCommand(NetworkCommand.ProcessNodeQueue processNodeQueue)
		{
			return new ProcessNodeQueueCommandTransport
			{
				NodeID = processNodeQueue.NodeId.Value,
			};
		}
		#endregion

		public NetworkCommand Map(PacketFlowMessage message)
		{
			switch ((TransportCommandPayloadType)message.payloadType)
			{
				case TransportCommandPayloadType.AddGatewayNode:				return GetCommandForAddGatewayNodePayload(message.payload);
				case TransportCommandPayloadType.AddRouterNode:					return GetCommandForAddRouterNodePayload(message.payload);
				case TransportCommandPayloadType.AddConsumerNode:				return GetCommandForAddConsumerNodePayload(message.payload);
				case TransportCommandPayloadType.LinkNodes:						return GetCommandForLinkNodesPayload(message.payload);
				case TransportCommandPayloadType.AddPacket:						return GetCommandForAddPacketPayload(message.payload);
				case TransportCommandPayloadType.IncrementPacketTypeDirection:	return GetCommandForIncrementPacketTypeDirectionPayload(message.payload);
				case TransportCommandPayloadType.ProcessNodeQueue:				return GetCommandForProcessNodeQueuePayload(message.payload);
				default:														throw new System.Exception("Taylor messed up a mapper... maybe. Or someone else made a breaking change. Someone.");
			}
		}

		#region Get Commands From Payloads
		private NetworkCommand.AddGatewayNode GetCommandForAddGatewayNodePayload(string payload)
		{
			var transport = JsonUtility.FromJson<AddGatewayNodeCommandTransport>(payload);
			return new NetworkCommand.AddGatewayNode
				(
					new NodeIdentifier(transport.NodeID),
					new NodePosition(transport.X, transport.Y),
					transport.Capacity
				);
		}

		private NetworkCommand.AddRouterNode GetCommandForAddRouterNodePayload(string payload)
		{
			var transport = JsonUtility.FromJson<AddRouterNodeCommandTransport>(payload);
			return new NetworkCommand.AddRouterNode
				(
					new NodeIdentifier(transport.NodeID),
					new NodePosition(transport.X, transport.Y),
					transport.Capacity
				);
		}

		private NetworkCommand.AddConsumerNode GetCommandForAddConsumerNodePayload(string payload)
		{
			var transport = JsonUtility.FromJson<AddConsumerNodeCommandTransport>(payload);
			return new NetworkCommand.AddConsumerNode
				(
					new NodeIdentifier(transport.NodeID),
					new NodePosition(transport.X, transport.Y),
					transport.Capacity
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
					(PortDirection)transport.SinkPortDirection,
					new LinkAttributes(transport.Bandwidth, transport.Latency)
				);
		}

		private NetworkCommand.AddPacket GetCommandForAddPacketPayload(string payload)
		{
			var transport = JsonUtility.FromJson<AddPacketCommandTransport>(payload);
			return new NetworkCommand.AddPacket
				(
					new PacketIdentifier(transport.PacketID),
					(PacketType)transport.Type,
					new NodeIdentifier(transport.NodeID)
				);
		}

		private NetworkCommand.IncrementPacketTypeDirection GetCommandForIncrementPacketTypeDirectionPayload(string payload)
		{
			var transport = JsonUtility.FromJson<IncrementPacketTypeDirectionCommandTransport>(payload);
			return new NetworkCommand.IncrementPacketTypeDirection
				(
					new NodeIdentifier(transport.NodeID),
					(PacketType)transport.Type
				);
		}

		private NetworkCommand.ProcessNodeQueue GetCommandForProcessNodeQueuePayload(string payload)
		{
			var transport = JsonUtility.FromJson<ProcessNodeQueueCommandTransport>(payload);
			return new NetworkCommand.ProcessNodeQueue
				(
					new NodeIdentifier(transport.NodeID)
				);
		}
		#endregion
	}
}
