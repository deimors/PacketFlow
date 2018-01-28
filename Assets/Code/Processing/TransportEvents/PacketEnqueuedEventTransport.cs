using System;

namespace Assets.Code.Processing.TransportEvents
{
	public class PacketEnqueuedEventTransport
	{
		public SerializableGuid NodeID;
		public SerializableGuid PacketID;
	}
}