using Functional.Maybe;
using PacketFlow.Domain;
using System;
using System.Collections.Generic;
using UniRx;

namespace PacketFlow.UseCases
{
	public class PacketTypeReadModel
	{
		private readonly Dictionary<PacketIdentifier, PacketType> _packetTypes = new Dictionary<PacketIdentifier, PacketType>();

		public PacketTypeReadModel(IObservable<NetworkEvent> networkEvents)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketAdded>()
				.Subscribe(packetAdded => _packetTypes[packetAdded.Packet.Id] = packetAdded.Packet.Type);
		}

		public PacketType this[PacketIdentifier packetId]
			=> _packetTypes.Lookup(packetId).OrElseDefault();
	}
}