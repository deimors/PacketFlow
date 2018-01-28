using PacketFlow.Domain;
using System.Collections.Generic;
using UnityEngine;

namespace PacketFlow.UseCases
{
	public interface IDisplayPacketEnqueue
	{
		void EnqueuePacket(PacketType color);
		void DequeuePacket();
	}
}