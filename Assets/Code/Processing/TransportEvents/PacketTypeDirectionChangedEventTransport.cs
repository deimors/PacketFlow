using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Processing.TransportEvents
{
	public class PacketTypeDirectionChangedEventTransport
	{
			public Guid NodeID;
			public int PacketType;
			public int PortDirection;
	}
}
