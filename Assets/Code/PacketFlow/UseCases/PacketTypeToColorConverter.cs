using PacketFlow.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.PacketFlow.UseCases
{
	public class PacketTypeToColorConverter
	{
		public Color GetPacketTypeColour(PacketType type)
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
}
