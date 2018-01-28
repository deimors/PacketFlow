namespace Assets.Code.Processing.TransportCommands
{
	public class LinkNodesCommandTransport
	{
		public SerializableGuid SourceID;
		public int SourcePortDirection;
		public SerializableGuid SinkID;
		public int SinkPortDirection;
		public int Bandwidth;
		public float Latency;
	}
}
