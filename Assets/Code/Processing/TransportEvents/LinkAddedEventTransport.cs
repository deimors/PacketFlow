namespace Assets.Code.Processing.TransportEvents
{
	public class LinkAddedEventTransport
	{
		public SerializableGuid LinkID;
		public SerializableGuid SourceID;
		public SerializableGuid SinkID;
	}
}