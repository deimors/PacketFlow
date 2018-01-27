using PacketFlow.Actors;
using PacketFlow.Domain;
using PacketFlow.Presentation.Node;
using System;
using UniRx;

namespace PacketFlow.UseCases
{
	public class CreateNodeAfterDelay
	{
		public CreateNodeAfterDelay(IEnqueueCommand<NetworkCommand> commandQueue)
		{
			Observable.Timer(TimeSpan.FromSeconds(1))
				.Subscribe(_ => commandQueue.Enqueue(BuildAddNodeCommand()));
		}

		private NetworkCommand BuildAddNodeCommand()
			=> new NetworkCommand.AddNode(new NodeIdentifier(), new NodePosition(0, 0), 5);
	}

	public class InstantiateNodeContainerWhenNodeAdded
	{
		public InstantiateNodeContainerWhenNodeAdded(IObservable<NetworkEvent> networkEvents, NodeContainer.Factory nodeFactory)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.NodeAdded>()
				.Subscribe(nodeAdded => CreateNodeContainer(nodeFactory, nodeAdded));
		}

		private static void CreateNodeContainer(NodeContainer.Factory nodeFactory, NetworkEvent.NodeAdded nodeAdded)
		{
			nodeFactory.Create(nodeAdded.Node.Id);
		}
	}
}