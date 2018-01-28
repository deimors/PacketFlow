using Functional.Maybe;
using PacketFlow.Domain;
using PacketFlow.UseCases;
using System;
using UniRx;
using UnityEngine;


namespace Assets.Code.PacketFlow.UseCases
{
	class UpdateQueueOnPacketEnqueued
	{
		public UpdateQueueOnPacketEnqueued(NodeIdentifier nodeId, IObservable<NetworkEvent> networkEvents, IConsumerPacketQueue nodePacketQueue)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketEnqueued>()
				.Where(packetEnqued => packetEnqued.NodeId == nodeId)
				.Subscribe(packetEnqued => nodePacketQueue.Enqueue(packetEnqued.PacketId);
		}
	}
}
