using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using UniRx;

namespace PacketFlow.UseCases
{
	public class AddPacketsPeriodically
	{
		public AddPacketsPeriodically(NodeIdentifier nodeId, IEnqueueCommand<NetworkCommand> commandQueue)
		{
			Observable.Interval(TimeSpan.FromSeconds(2))
				.Subscribe(_ => commandQueue.Enqueue(new NetworkCommand.AddPacket(new PacketIdentifier(), PacketType.Red, nodeId)));
		}
	}
}