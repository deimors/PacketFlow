using System;

namespace Assets.Code.Processing.TransportCommands
{
	public enum TransportCommandPayloadType
	{
		AddGatewayNode = 0,
		AddRouterNode,
		AddConsumerNode,
		LinkNodes,
		AddPacket,
		IncrementPacketTypeDirection,
		ProcessNodeQueue,
		CompleteTransmission
	}

	public class AddGatewayNodeCommandTransport
	{
		public SerializableGuid NodeID;
		public float X;
		public float Y;
		public int Capacity;
	}

	public class AddRouterNodeCommandTransport
	{
		public SerializableGuid NodeID;
		public float X;
		public float Y;
		public int Capacity;
	}

	public class AddConsumerNodeCommandTransport
	{
		public SerializableGuid NodeID;
		public float X;
		public float Y;
		public int Capacity;
	}

	public class LinkNodesCommandTransport
	{
		public SerializableGuid SourceID;
		public int SourcePortDirection;
		public SerializableGuid SinkID;
		public int SinkPortDirection;
		public int Bandwidth;
		public float Latency;
	}

	public class AddPacketCommandTransport
	{
		public SerializableGuid PacketID;
		public int Type;
		public SerializableGuid NodeID;
	}

	public class IncrementPacketTypeDirectionCommandTransport
	{
		public SerializableGuid NodeID;
		public int Type;
	}

	public class ProcessNodeQueueCommandTransport
	{
		public SerializableGuid NodeID;
	}

	public class CompleteTransmissionCommandTransport
	{
		public SerializableGuid PacketID;
		public SerializableGuid LinkID;
	}
	
}
