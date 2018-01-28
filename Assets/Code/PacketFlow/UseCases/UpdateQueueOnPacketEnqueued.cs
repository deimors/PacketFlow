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
		private readonly PacketTypeToColorConverter packetTypeToColorConverter = new PacketTypeToColorConverter();

		public UpdateQueueOnPacketEnqueued(NodeIdentifier nodeId, IObservable<NetworkEvent> networkEvents, PacketTypeReadModel packetTypeReadModel)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketEnqueued>()
				.Where(packetEnqued => packetEnqued.NodeId == nodeId)
				.Subscribe(packetEnqued => GetPacketColorFromType(packetTypeReadModel[packetEnqued.PacketId]));
		}

		private Color GetPacketColorFromType(PacketType packetType)
		{
			return packetTypeToColorConverter.GetPacketTypeColour(packetType)
		}
	}
}
