using System;
using System.Linq;

namespace PacketFlow.Domain
{
	public class RouterState
	{
		private readonly PortDirection[] _packetTypeToPortMap;

		public RouterState()
		{
			_packetTypeToPortMap = Enumerable.Repeat(PortDirection.Top, Enum.GetValues(typeof(PacketType)).Length).ToArray();
		}

		public RouterState(PortDirection[] packetTypeToPortMap)
		{
			_packetTypeToPortMap = packetTypeToPortMap;
		}

		public PortDirection this[PacketType packetType]
			=> _packetTypeToPortMap[(int)packetType];

		public RouterState WithPacketDirection(PacketType packetType, PortDirection port)
			=> new RouterState(
				_packetTypeToPortMap
					.Select((p, i) => i == (int)packetType ? port : p)
					.ToArray()
			);
	}
}
