using System;
using System.Collections.Immutable;

namespace PacketFlow.Domain
{
	public class Link
	{
		public LinkIdentifier Id { get; }
		public NodeIdentifier Source { get; }
		public NodeIdentifier Sink { get; }
		public LinkAttributes Attributes { get; }
		public ImmutableList<PacketIdentifier> Content { get; }

		public bool IsFull => Content.Count == Attributes.Bandwidth;

		public Link(LinkIdentifier id, NodeIdentifier source, NodeIdentifier sink, LinkAttributes attributes, ImmutableList<PacketIdentifier> content)
		{
			Id = id ?? throw new ArgumentNullException(nameof(id));
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Sink = sink ?? throw new ArgumentNullException(nameof(sink));
			Attributes = attributes ?? throw new ArgumentNullException(nameof(attributes));
			Content = content ?? throw new ArgumentNullException(nameof(content));
		}

		public Link Add(PacketIdentifier linkId)
			=> new Link(Id, Source, Sink, Attributes, Content.Add(linkId));

		public Link Remove(PacketIdentifier linkId)
			=> new Link(Id, Source, Sink, Attributes, Content.Remove(linkId));
	}
}
