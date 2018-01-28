using PacketFlow.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace PacketFlow.UseCases
{
	public class DisplayArrowDirectionWhenPacketTypeDirectionChanged
	{
		public DisplayArrowDirectionWhenPacketTypeDirectionChanged(IObservable<NetworkEvent> networkEvents, NodeIdentifier nodeId, IDisplayArrowDirection arrowDirection)
		{
			networkEvents.OfType<NetworkEvent, NetworkEvent.PacketTypeDirectionChanged>()
				.Where(directionChanged => directionChanged.NodeId.Equals(nodeId))
				.Subscribe(directionChanged => arrowDirection.Display(directionChanged.PacketType, directionChanged.Port));
		}
	}
}
