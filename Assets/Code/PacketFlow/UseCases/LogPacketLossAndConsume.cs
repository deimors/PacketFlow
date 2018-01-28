using PacketFlow.Domain;
using System;
using UniRx;
using UnityEngine;

namespace PacketFlow.UseCases
{
	public class LogPacketLossAndConsume
	{
		public LogPacketLossAndConsume(IObservable<NetworkEvent> networkEvents)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketLost>()
				.Subscribe(packetLost => Debug.Log($"Packet Loss!!! {packetLost.PacketId}"));

			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketConsumed>()
				.Subscribe(packetConsumed => Debug.Log($"Packet Consumed!!! {packetConsumed.PacketId}"));
		}
	}
}