using Functional.Maybe;
using PacketFlow.Domain;
using System;
using UniRx;
using UnityEngine;


namespace PacketFlow.UseCases
{
	public class DisplayOnPacketEnqueued
	{
		public DisplayOnPacketEnqueued(NodeIdentifier nodeId, IObservable<NetworkEvent> networkEvents, PacketTypeReadModel packetTypeReadModel, IDisplayPacketEnqueue consumerPacket)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketEnqueued>()
				.Where(packetEnqued => packetEnqued.NodeId == nodeId)
				.Select(packetEnqued => packetTypeReadModel[packetEnqued.PacketId])
				.Do(packetEnqueued => Debug.Log($"Packet Enqueued {packetEnqueued}"))
				.Subscribe(color => consumerPacket.EnqueuePacket(color));

		}
	}
}
