using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using UniRx;

namespace PacketFlow.UseCases
{
	public class AddPacketsPeriodically
	{
		private static readonly int PacketTypes = Enum.GetValues(typeof(PacketType)).Length;

		public AddPacketsPeriodically(NodeIdentifier nodeId, IEnqueueCommand<NetworkCommand> commandQueue)
		{
			Observable.Interval(TimeSpan.FromSeconds(2))
				.Subscribe(_ => commandQueue.Enqueue(new NetworkCommand.AddPacket(new PacketIdentifier(), RandomPacketType, nodeId)));
		}

		private PacketType RandomPacketType
			=> (PacketType)UnityEngine.Random.Range(0, PacketTypes);
	}
}