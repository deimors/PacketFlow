using Functional.Maybe;
using PacketFlow.Domain;
using PacketFlow.UseCases;
using System;
using UniRx;
using UnityEngine;


namespace PacketFlow.UseCases
{
	public class DisplayOnPacketDequeued
	{
		public DisplayOnPacketDequeued(NodeIdentifier nodeId, IObservable<NetworkEvent> networkEvents, PacketTypeReadModel packetTypeReadModel, IDisplayPacketEnqueue consumerPacket)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketDequeued>()
				.Where(packetDequed => packetDequed.NodeId == nodeId)
				.Subscribe(color => consumerPacket.DequeuePacket());
		}
	}
}
