using PacketFlow.Actors;
using PacketFlow.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace PacketFlow.UseCases
{
	public class IncrementPacketTypeDirectionWhenArrowClicked
	{
		public IncrementPacketTypeDirectionWhenArrowClicked(IArrowClickedObservable arrowClicks, NodeIdentifier nodeId, IEnqueueCommand<NetworkCommand> enqueueCommand)
		{
			arrowClicks
				.Do(packetType => Debug.Log($"Packet Type: '{packetType}'"))
				.Select(packetType => BuildCommand(packetType, nodeId))
				.Subscribe(enqueueCommand.Enqueue);
		}

		private NetworkCommand BuildCommand(PacketType packetType, NodeIdentifier nodeId)
		{
			return new NetworkCommand.IncrementPacketTypeDirection(nodeId, packetType);
		}
	}
}
