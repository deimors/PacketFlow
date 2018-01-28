using System;

namespace PacketFlow.Domain
{
	public class Link
	{
		public LinkIdentifier Id { get; }
		public NodeIdentifier Source { get; }
		public NodeIdentifier Sink { get; }

		public Link(LinkIdentifier id, NodeIdentifier source, NodeIdentifier sink)
		{
			Id = id ?? throw new ArgumentNullException(nameof(id));
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Sink = sink ?? throw new ArgumentNullException(nameof(sink));
		}
	}
}
