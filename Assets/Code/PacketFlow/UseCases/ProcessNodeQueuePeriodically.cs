using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using UniRx;

namespace PacketFlow.UseCases
{
	public class ProcessNodeQueuePeriodically
	{
		public ProcessNodeQueuePeriodically(NodeIdentifier nodeId, IEnqueueCommand<NetworkCommand> commandQueue)
		{
			Observable.Interval(TimeSpan.FromSeconds(1))
				.Subscribe(_ => commandQueue.Enqueue(new NetworkCommand.ProcessNodeQueue(nodeId)));
		}
	}
}