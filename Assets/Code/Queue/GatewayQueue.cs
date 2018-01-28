using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using PacketFlow.Domain;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Code.Queue
{
	public class GatewayQueue : MonoBehaviour
	{
		public int DisplaySize;
		private Queue<Packet> _packetQueue;
		private Queue<GameObject> _gameObjectQueue;

		public RectTransform ParentTransform;

		public GameObject PacketCreator;

		[Inject]
		public void Initialize(IObservable<NetworkEvent> networkEvents, NodeIdentifier nodeIdentifier)
		{
			_packetQueue = new Queue<Packet>();
			_gameObjectQueue = new Queue<GameObject>(DisplaySize);

			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketAdded>()
				.Subscribe(packetAdded => AddPacketToQueue(packetAdded.Packet));

			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketDequeued>()
				.Where(packetDequeued => packetDequeued.NodeId == nodeIdentifier)
				.Subscribe(_ => RemovePacketFromQueue());
		}

		private void RemovePacketFromQueue()
		{
			if (!_gameObjectQueue.IsEmpty())
				Destroy(_gameObjectQueue.Dequeue());

			UpdateGameObjectQueue();
		}

		private void AddPacketToQueue(Packet packet)
		{
			_packetQueue.Enqueue(packet);
			UpdateGameObjectQueue();
		}

		private void UpdateGameObjectQueue()
		{
			if (_gameObjectQueue.Count < 5 && _packetQueue.Any())
			{
				_gameObjectQueue.Enqueue(Create(_packetQueue.Dequeue()));
			}
		}

		public GameObject Create(Packet packet)
		{
			var square = Instantiate(PacketCreator, ParentTransform, false);
			var image = square.GetComponent<Image>();
			switch (packet.Type)
			{
				case PacketType.Red:
					image.color = Color.red;
					break;
				case PacketType.Green:
					image.color = Color.green;
					break;
				default:
					image.color = Color.blue;
					break;
			}

			return square;
		}
	}
}

