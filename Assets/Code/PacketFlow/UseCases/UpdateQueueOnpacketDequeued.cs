using Functional.Maybe;
using PacketFlow.Domain;
using PacketFlow.UseCases;
using System;
using UniRx;
using UnityEngine;


namespace Assets.Code.PacketFlow.UseCases
{
	class UpdateQueueOnPacketDequeued
	{
		public UpdateQueueOnPacketDequeued(NodeIdentifier nodeId, IObservable<NetworkEvent> networkEvents, PacketTypeReadModel packetTypeReadModel, IDisplayPacketEnqueue consumerPacket)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketEnqueued>()
				.Where(packetDequed => packetDequed.NodeId == nodeId)
				.Subscribe(color => consumerPacket.DequeuePacket());
		}
	}
}
