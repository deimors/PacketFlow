using System;

namespace Assets.Code.Processing.TransportEvents
{
	public class PortAssignedEventTransport
	{
		public SerializableGuid NodeID;
		public int PortDirection;
		public SerializableGuid LinkID;
		public int ConnectionDirection;
	}
}