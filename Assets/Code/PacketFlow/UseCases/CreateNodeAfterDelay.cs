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
			commandQueue.Enqueue(BuildAddNodeCommand(0, 0));
			commandQueue.Enqueue(BuildAddNodeCommand(4, 4));
		}

		private NetworkCommand BuildAddNodeCommand(float x, float y)
			=> new NetworkCommand.AddGatewayNode(new NodeIdentifier(), new NodePosition(x, y), 5);
	}
}