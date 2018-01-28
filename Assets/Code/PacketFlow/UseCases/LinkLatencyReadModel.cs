using Functional.Maybe;
using PacketFlow.Domain;
using System;
using System.Collections.Generic;
using UniRx;

namespace PacketFlow.UseCases
{
	public class LinkLatencyReadModel
	{
		private readonly Dictionary<LinkIdentifier, float> _latencies = new Dictionary<LinkIdentifier, float>();

		public LinkLatencyReadModel(IObservable<NetworkEvent> networkEvents)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.LinkAdded>()
				.Subscribe(linkAdded => _latencies[linkAdded.Link.Id] = linkAdded.Link.Attributes.Latency);
		}

		public float this[LinkIdentifier linkId]
			=> _latencies.Lookup(linkId).OrElseDefault();
	}
}