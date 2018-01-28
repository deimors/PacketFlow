using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using UniRx;
using UnityEngine;

namespace PacketFlow.UseCases
{
	public class AddPacketsPeriodically
	{
		public AddPacketsPeriodically(NodeIdentifier nodeId, IEnqueueCommand<NetworkCommand> commandQueue)
		{
			Observable.Interval(TimeSpan.FromSeconds(2))
				.Subscribe(_ => commandQueue.Enqueue(new NetworkCommand.AddPacket(new PacketIdentifier(), PacketType.Red, nodeId)));
		}
	}

	public class OnStartTransmission
	{
		public OnStartTransmission(LinkIdentifier linkId, IObservable<NetworkEvent> networkEvents)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketTransmissionStarted>()
				.Where(transmissionStarted => transmissionStarted.LinkId == linkId)
				.Subscribe(transmissionStarted => Debug.Log($"Transmit {transmissionStarted.PacketId} on {linkId}"));
		}
	}
}