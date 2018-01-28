namespace PacketFlow.Domain
{
	public class LinkAttributes
	{
		public LinkAttributes(int bandwidth, float latency)
		{
			Bandwidth = bandwidth;
			Latency = latency;
		}

		public int Bandwidth { get; }
		public float Latency { get; }
	}
}
