using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using UniRx;

namespace PacketFlow.UseCases
{
	public class CreateNodeAfterDelay
	{
		public CreateNodeAfterDelay(IEnqueueCommand<NetworkCommand> commandQueue)
		{
			Observable.Timer(TimeSpan.FromSeconds(1))
				.Subscribe(_ => BuildNetwork(commandQueue));
		}

		private void BuildNetwork(IEnqueueCommand<NetworkCommand> commandQueue)
		{
			var node1 = new NodeIdentifier();
			var node2 = new NodeIdentifier();
			var node3 = new NodeIdentifier();

			commandQueue.Enqueue(BuildAddNodeCommand(node1, 0, 0));
			commandQueue.Enqueue(BuildAddNodeCommand(node2, 4, 4));
			commandQueue.Enqueue(BuildAddNodeCommand(node3, -4, -4));

			commandQueue.Enqueue(new NetworkCommand.LinkNodes(node1, PortDirection.Right, node2, PortDirection.Bottom));
			commandQueue.Enqueue(new NetworkCommand.LinkNodes(node1, PortDirection.Bottom, node3, PortDirection.Right));
		}

		private NetworkCommand BuildAddNodeCommand(NodeIdentifier nodeId, float x, float y)
			=> new NetworkCommand.AddRouterNode(nodeId, new NodePosition(x, y), 5);
	}
}