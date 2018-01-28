using PacketFlow.Domain;
using System.Collections.Generic;
using UnityEngine;

namespace PacketFlow.UseCases
{
	public interface IConsumerPacketQueue
	{
		void Enqueue(GameObject gameObject);
		GameObject Dequeue();
	}
}