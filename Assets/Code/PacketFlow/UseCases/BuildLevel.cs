using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using UniRx;

namespace PacketFlow.UseCases
{
	public class BuildLevel
	{
		public BuildLevel(IEnqueueCommand<NetworkCommand> commandQueue)
		{
			Observable.Timer(TimeSpan.FromSeconds(1))
				.Subscribe(_ => BuildNetwork(commandQueue));
		}

		private void BuildNetwork(IEnqueueCommand<NetworkCommand> commandQueue)
		{
			var gateway = new NodeIdentifier();
			var router = new NodeIdentifier();
			var consumer1 = new NodeIdentifier();
			var consumer2 = new NodeIdentifier();
			var consumer3 = new NodeIdentifier();

			var linkAttributes = new LinkAttributes(2, 2f);

			commandQueue.Enqueue(new NetworkCommand.AddGatewayNode(gateway, new NodePosition(0, 4), 5));
			commandQueue.Enqueue(new NetworkCommand.AddRouterNode(router, new NodePosition(0, 0), 5));
			commandQueue.Enqueue(new NetworkCommand.AddConsumerNode(consumer1, new NodePosition(4, -4), 5));
			commandQueue.Enqueue(new NetworkCommand.AddConsumerNode(consumer2, new NodePosition(-4, -4), 5));
			commandQueue.Enqueue(new NetworkCommand.AddConsumerNode(consumer3, new NodePosition(0, -4), 5));

			commandQueue.Enqueue(new NetworkCommand.LinkNodes(gateway, PortDirection.Bottom, router, PortDirection.Top, linkAttributes));
			commandQueue.Enqueue(new NetworkCommand.LinkNodes(router, PortDirection.Right, consumer1, PortDirection.Left, linkAttributes));
			commandQueue.Enqueue(new NetworkCommand.LinkNodes(router, PortDirection.Left, consumer2, PortDirection.Right, linkAttributes));
			commandQueue.Enqueue(new NetworkCommand.LinkNodes(router, PortDirection.Bottom, consumer3, PortDirection.Top, linkAttributes));

		}
	}
}