using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using PacketFlow.Domain;
using PacketFlow.UseCases;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Queue
{
	public class ConsumerQueue : GatewayQueue, IDisplayPacketEnqueue
	{
		public const int MaxQueueSize = 5;
		
		protected override void InitializeEventListeners(IObservable<NetworkEvent> networkEvents)
		{
			
		}

		public void EnqueuePacket(PacketType packetType)
		{
			AddPacketToQueue(new Packet(new PacketIdentifier(), packetType));
		}

		public void DequeuePacket()
		{
			RemovePacketFromQueue();

		}
	}
}
