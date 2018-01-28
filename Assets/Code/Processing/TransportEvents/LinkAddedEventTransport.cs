using System.Collections.Generic;

namespace Assets.Code.Processing.TransportEvents
{
	public class LinkAddedEventTransport
	{
		public SerializableGuid LinkID;
		public SerializableGuid SourceID;
		public SerializableGuid SinkID;
		public int Bandwidth;
		public float Latency;
		public List<SerializableGuid> Content;
	}
}