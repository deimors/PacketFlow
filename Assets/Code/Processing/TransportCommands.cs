using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Processing
{
	public class TransportCommands
	{
		public enum TransportCommandPayloadType
		{
			AddNode = 0,
			LinkNodes = 1,
			AddPacket = 2
		}

		public class AddNodeCommandTransport
		{
			public Guid ID;
			public float X;
			public float Y;
			public int Capacity;
			public int NodeType;
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
			public Guid NodeId;
		}

	}
}
