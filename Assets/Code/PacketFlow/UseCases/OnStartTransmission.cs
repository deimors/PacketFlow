using Functional.Maybe;
using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using UniRx;
using UnityEngine;

namespace PacketFlow.UseCases
{
	public class OnStartTransmission
	{
		private readonly IDisplayPacketTransmission displayTransmission;
		private readonly LinkLatencyReadModel linkLatency;
		private readonly IEnqueueCommand<NetworkCommand> commandQueue;

		public OnStartTransmission(LinkIdentifier linkId, IObservable<NetworkEvent> networkEvents, IDisplayPacketTransmission displayTransmission, LinkLatencyReadModel linkLatency, IEnqueueCommand<NetworkCommand> commandQueue)
		{
			this.displayTransmission = displayTransmission;
			this.linkLatency = linkLatency;
			this.commandQueue = commandQueue;
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketTransmissionStarted>()
				.Where(transmissionStarted => transmissionStarted.LinkId == linkId)
				.Subscribe(transmissionStarted => TransmitPacket(transmissionStarted.PacketId, transmissionStarted.LinkId));
			
		}

		private void TransmitPacket(PacketIdentifier packetId, LinkIdentifier linkId)
		{
			float delay = linkLatency[linkId];

			displayTransmission.Display(packetId, delay);

			Observable.Timer(TimeSpan.FromSeconds(delay))
				.Subscribe(_ => CompleteTransmission(packetId, linkId));
		}

		private void CompleteTransmission(PacketIdentifier packetId, LinkIdentifier linkId)
		{
			Debug.Log($"CompleteTransmission {packetId}");
			commandQueue.Enqueue(new NetworkCommand.CompleteTransmission(packetId, linkId));
		}
	}
}