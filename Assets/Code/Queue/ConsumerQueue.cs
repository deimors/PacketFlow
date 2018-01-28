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
		private Queue<GameObject> _gameObjectQueue;

		void Start()
		{
			_gameObjectQueue = new Queue<GameObject>(MaxQueueSize);

			//AddPacket.onClick
			//	.AsObservable()
			//	.Subscribe(_ => AddPacketToNodeQueue(TempGetPacket()));

			//RemovePacket.onClick
			//	.AsObservable()
			//	.Subscribe(_ => RemovePacketFromNodeQueue());
		}

		public void EnqueuePacket(PacketType packetType)
		{
			AddPacketToNodeQueue(new Packet(new PacketIdentifier(), packetType));
		}

		public void DequeuePacket()
		{
			RemovePacketFromNodeQueue();

		}

		private void RemovePacketFromNodeQueue()
		{
			if (!_gameObjectQueue.IsEmpty())
				Destroy(_gameObjectQueue.Dequeue());
		}

		private void AddPacketToNodeQueue(Packet packet)
		{
			if (_gameObjectQueue.Count < MaxQueueSize)
				_gameObjectQueue.Enqueue(Create(packet));
		}
	}
}
