using System;

namespace Assets.Code.Processing.TransportEvents
{
	public class PacketTransmissionStartedEventTransport
	{
		public SerializableGuid PacketID;
		public SerializableGuid LinkID;
	}
}