using Assets.Code.PacketFlow.UseCases;
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
		private readonly PacketTypeToColorConverter packetTypeToColorConverter = new PacketTypeToColorConverter();

		public SetPacketColourWhenPacketAdded(PacketIdentifier packetId, IObservable<NetworkEvent> networkEvents, IDisplayPacketColour displayColour)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.PacketAdded>()
				.Where(packetAdded => packetAdded.Packet.Id == packetId)
				.Subscribe(packetAdded => displayColour.Display(GetPacketTypeColour(packetAdded.Packet.Type)));
		}

		private Color GetPacketTypeColour(PacketType type)
		{
			return packetTypeToColorConverter.GetPacketTypeColour(type);
		}
	}

	public interface IDisplayPacketColour
	{
		void Display(Color color);
	}
}
