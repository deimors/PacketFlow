using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Processing
{
	public class TransportCommands
	{
		public class AddNodeCommand
		{
			public Guid ID;
			public float X;
			public float Y;
			public int Capacity;
			public int NodeType;
		}

		public class LinkNodeCommand
		{
			public Guid SourceID;
			public int SourcePortDirection;
			public Guid SinkID;
			public int SinkPortDirection;
		}

		public class AddPacketCommand
		{
			public Guid PacketID;
			public int Type;
			public Guid NodeId;
		}

	}
}
