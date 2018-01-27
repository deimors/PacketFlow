﻿using System;

namespace PacketFlow.Domain
{
	public class LinkIdentifier : IEquatable<LinkIdentifier>
	{
		public LinkIdentifier() : this(Guid.NewGuid()) { }

		public LinkIdentifier(Guid value)
		{
			Value = value;
		}

		public Guid Value { get; }

		public override bool Equals(object obj) 
			=> Equals(obj as LinkIdentifier);

		public bool Equals(LinkIdentifier other) 
			=> other != null && Value.Equals(other.Value);

		public override int GetHashCode() 
			=> -1937169414 + Value.GetHashCode();

		public static bool operator ==(LinkIdentifier identifier1, LinkIdentifier identifier2) 
			=> Equals(identifier1, identifier2);

		public static bool operator !=(LinkIdentifier identifier1, LinkIdentifier identifier2) 
			=> !(identifier1 == identifier2);

		public override string ToString()
			=> $"(Link {Value})";
	}

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
