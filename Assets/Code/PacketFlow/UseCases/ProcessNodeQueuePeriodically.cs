using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using UniRx;

namespace PacketFlow.UseCases
{
	public class ProcessNodeQueuePeriodically
	{
		public ProcessNodeQueuePeriodically(NodeIdentifier nodeId, IEnqueueCommand<NetworkCommand> commandQueue, TimeSpan period)
		{
			Observable.Interval(period)
				.Subscribe(_ => commandQueue.Enqueue(new NetworkCommand.ProcessNodeQueue(nodeId)));
		}
	}
}