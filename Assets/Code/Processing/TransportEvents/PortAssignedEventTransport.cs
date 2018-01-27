using System;

namespace Assets.Code.Processing.TransportEvents
{
	public class PortAssignedEventTransport
	{
		public Guid NodeID;
		public int PortDirection;
		public Guid LinkID;
		public int ConnectionDirection;
	}
}