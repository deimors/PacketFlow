using PacketFlow.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace PacketFlow.UseCases
{
	public class SetPacketColourWhenPacketAdded
	{
		public SetPacketColourWhenPacketAdded(PacketIdentifier packetId, IObservable<NetworkEvent> networkEvents, IDisplayPacketColour displayColour)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketAdded>()
				.Where(packetAdded => packetAdded.Packet.Id == packetId)
				.Subscribe(packetAdded => displayColour.Display(GetPacketTypeColour(packetAdded.Packet.Type)));
		}

		private Color GetPacketTypeColour(PacketType type)
		{
			switch (type)
			{
				case PacketType.Red: return Color.red;
				case PacketType.Blue: return Color.blue;
				case PacketType.Green: return Color.green;
				default: return Color.grey;
			}
		}
	}

	public interface IDisplayPacketColour
	{
		void Display(Color color);
	}
}
