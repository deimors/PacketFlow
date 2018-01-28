using System.Collections.Generic;
using System.Linq;
using ModestTree;
using PacketFlow.Domain;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Queue
{
	public class ConsumerQueue : GatewayQueue
	{
		public const int MaxQueueSize = 5;
		private Queue<GameObject> _gameObjectQueue;

		 void Start()
		{
			_gameObjectQueue = new Queue<GameObject>(MaxQueueSize);

			AddPacket.onClick
				.AsObservable()
				.Subscribe(_ => AddPacketToNodeQueue(TempGetPacket()));

			RemovePacket.onClick
				.AsObservable()
				.Subscribe(_ => RemovePacketFromNodeQueue());
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
