using System;

namespace Assets.Code.Processing
{
	public class TransportCommands
	{
		public enum TransportCommandPayloadType
		{
			AddGatewayNode = 0,
			AddRouterNode,
			AddConsumerNode,
			LinkNodes,
			AddPacket,
			IncrementPacketTypeDirection
		}

		public class AddGatewayNodeCommandTransport
		{
			public Guid NodeID;
			public float X;
			public float Y;
			public int Capacity;
		}

		public class AddRouterNodeCommandTransport
		{
			public Guid NodeID;
			public float X;
			public float Y;
			public int Capacity;
		}

		public class AddConsumerNodeCommandTransport
		{
			public Guid NodeID;
			public float X;
			public float Y;
			public int Capacity;
		}

		public class LinkNodesCommandTransport
		{
			public Guid SourceID;
			public int SourcePortDirection;
			public Guid SinkID;
			public int SinkPortDirection;
		}

		public class AddPacketCommandTransport
		{
			public Guid PacketID;
			public int Type;
			public Guid NodeID;
		}

		public class IncrementPacketTypeDirectionCommandTransport
		{
			public Guid NodeID;
			public int Type;
		}
	}
}
