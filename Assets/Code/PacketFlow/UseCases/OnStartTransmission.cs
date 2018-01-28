using PacketFlow.Domain;
using System;
using UniRx;
using UnityEngine;

namespace PacketFlow.UseCases
{
	public class OnStartTransmission
	{
		public OnStartTransmission(LinkIdentifier linkId, IObservable<NetworkEvent> networkEvents, IDisplayPacketTransmission displayTransmission, LinkLatencyReadModel linkLatency)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketTransmissionStarted>()
				.Where(transmissionStarted => transmissionStarted.LinkId == linkId)
				.Subscribe(transmissionStarted => displayTransmission.Display(PacketType.Red, linkLatency[transmissionStarted.LinkId]));
		}
	}
}