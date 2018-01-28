using PacketFlow.Domain;
using PacketFlow.UseCases;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Code.PacketFlow.Presentation.ConsumerQueue
{
	class ConsuerQueuePresenter : MonoBehaviour, IConsumerPacketQueue
	{
		public Queue<GameObject> ConsumerPacketQueue;

		public GameObject Dequeue()
		{
			return ConsumerPacketQueue.Dequeue();
		}

		public void Enqueue(GameObject gameObject)
		{
			ConsumerPacketQueue.Enqueue(gameObject);
		}
	}
}