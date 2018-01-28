using UnityEngine;
using static Assets.Code.Constants;
using PacketFlow.Domain;
using System;
using Assets.Code.Processing.TransportCommands;

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
					pnq => (int)TransportCommandPayloadType.ProcessNodeQueue,
					ct => (int)TransportCommandPayloadType.CompleteTransmission
				),
				payload = JsonUtility.ToJson(
						command.Match(
						agn => GetPayloadForAddGatewayNodeCommand(agn),
						arn => GetPayloadForAddRouterNodeCommand(arn),
						acn => GetPayloadForAddConsumerNodeCommand(acn),
						ln => GetPayloadForLinkNodesCommand(ln),
						ap => GetPayloadForAddPacketCommand(ap),
						iptd => GetPayloadForIncrementPacketTypeDirectionCommand(iptd),
						pnq => GetPayloadForProcessNodeQueueCommand(pnq),
						ct => GetPayloadForCompleteTransmissionCommand(ct)
					)
				)
			};
		}

		#region Get Payloads for Commands
		private object GetPayloadForAddGatewayNodeCommand (NetworkCommand.AddGatewayNode command)
		{
			return new AddGatewayNodeCommandTransport
			{
				NodeID = new SerializableGuid(command.NodeId.Value),
				X = command.Position.X,
				Y = command.Position.Y,
				Capacity = command.Capacity
			};
		}

		private object GetPayloadForAddRouterNodeCommand(NetworkCommand.AddRouterNode command)
		{
			return new AddRouterNodeCommandTransport
			{
				NodeID = new SerializableGuid(command.NodeId.Value),
				X = command.Position.X,
				Y = command.Position.Y,
				Capacity = command.Capacity
			};
		}

		private object GetPayloadForAddConsumerNodeCommand(NetworkCommand.AddConsumerNode command)
		{
			return new AddConsumerNodeCommandTransport
			{
				NodeID = new SerializableGuid(command.NodeId.Value),
				X = command.Position.X,
				Y = command.Position.Y,
				Capacity = command.Capacity
			};
		}

		private object GetPayloadForLinkNodesCommand(NetworkCommand.LinkNodes command)
		{
			return new LinkNodesCommandTransport
			{
				SourceID = new SerializableGuid(command.Source.Value),
				SourcePortDirection = (int)command.SourcePort,
				SinkID = new SerializableGuid(command.Sink.Value),
				SinkPortDirection = (int)command.SinkPort,
				Bandwidth = command.Attributes.Bandwidth,
				Latency = command.Attributes.Latency
			};
		}		

		private object GetPayloadForAddPacketCommand(NetworkCommand.AddPacket command)
		{
			return new AddPacketCommandTransport
			{
				NodeID = new SerializableGuid(command.NodeId.Value),
				Type = (int)command.Type,
				PacketID = new SerializableGuid(command.PackedId.Value)
			};
		}

		private object GetPayloadForIncrementPacketTypeDirectionCommand(NetworkCommand.IncrementPacketTypeDirection command)
		{
			return new IncrementPacketTypeDirectionCommandTransport
			{
				NodeID = new SerializableGuid(command.NodeId.Value),
				Type = (int)command.PacketType
			};
		}

		private object GetPayloadForProcessNodeQueueCommand(NetworkCommand.ProcessNodeQueue command)
		{
			return new ProcessNodeQueueCommandTransport
			{
				NodeID = new SerializableGuid(command.NodeId.Value),
			};
		}

		private object GetPayloadForCompleteTransmissionCommand(NetworkCommand.CompleteTransmission command)
		{
			return new CompleteTransmissionCommandTransport
			{
				PacketID = new SerializableGuid(command.PacketId.Value),
				LinkID = new SerializableGuid(command.LinkId.Value)
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
				case TransportCommandPayloadType.CompleteTransmission:			return GetCommandForCompleteTransmissionPayload(message.payload);
				default:														throw new NotImplementedException();
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

		private NetworkCommand.CompleteTransmission GetCommandForCompleteTransmissionPayload(string payload)
		{
			var transport = JsonUtility.FromJson<CompleteTransmissionCommandTransport>(payload);
			return new NetworkCommand.CompleteTransmission
				(
					new PacketIdentifier(transport.PacketID),
					new LinkIdentifier(transport.LinkID)
				);
		}
		#endregion
	}
}
