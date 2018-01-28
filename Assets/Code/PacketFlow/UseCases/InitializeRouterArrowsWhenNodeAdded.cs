using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace PacketFlow.UseCases
{
	public class InitializeRouterArrowsWhenLinkAddedAsSource
	{
		private readonly IEnqueueCommand<NetworkCommand> commandQueue;

		public InitializeRouterArrowsWhenLinkAddedAsSource(NodeIdentifier nodeId, IObservable<NetworkEvent> networkEvents, IEnqueueCommand<NetworkCommand> commandQueue)
		{
			this.commandQueue = commandQueue;

			networkEvents
				.OfType<NetworkEvent, NetworkEvent.LinkAdded>()
				.Where(linkAdded => linkAdded.Link.Source == nodeId)
				.Subscribe(linkAdded => Initialize(nodeId, linkAdded.Link.Id));
		}

		private void Initialize(NodeIdentifier nodeId, LinkIdentifier linkId)
		{
			commandQueue.Enqueue(new NetworkCommand.IncrementPacketTypeDirection(nodeId, PacketType.Red));
			commandQueue.Enqueue(new NetworkCommand.IncrementPacketTypeDirection(nodeId, PacketType.Blue));
			commandQueue.Enqueue(new NetworkCommand.IncrementPacketTypeDirection(nodeId, PacketType.Green));
		}
	}
}
