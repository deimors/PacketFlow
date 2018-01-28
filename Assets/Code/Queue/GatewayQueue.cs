using System.Collections.Generic;
using System.Linq;
using ModestTree;
using PacketFlow.Domain;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Queue
{
	public class GatewayQueue : MonoBehaviour
	{
		public int DisplaySize;
		private Queue<Packet> _packetQueue;
		private Queue<GameObject> _gameObjectQueue;

		public Button AddPacket;

		public Button RemovePacket;

		public RectTransform ParentTransform;

		public GameObject PacketCreator;

		void Start()
		{
			_packetQueue = new Queue<Packet>();
			_gameObjectQueue = new Queue<GameObject>(DisplaySize);

			AddPacket.onClick
				.AsObservable()
				.Subscribe(_ => AddPacketToQueue(TempGetPacket()));

			RemovePacket.onClick
				.AsObservable()
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

		public Packet TempGetPacket() => new Packet(new PacketIdentifier(), (PacketType)(new System.Random().Next(-1,3)));

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

