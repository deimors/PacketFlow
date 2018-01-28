using System;

namespace PacketFlow.Domain
{
	public class Link
	{
		public LinkIdentifier Id { get; }
		public NodeIdentifier Source { get; }
		public NodeIdentifier Sink { get; }
		public LinkAttributes Attributes { get; }

		public Link(LinkIdentifier id, NodeIdentifier source, NodeIdentifier sink, LinkAttributes attributes)
		{
			Id = id ?? throw new ArgumentNullException(nameof(id));
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Sink = sink ?? throw new ArgumentNullException(nameof(sink));
			Attributes = attributes;
		}
	}

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
