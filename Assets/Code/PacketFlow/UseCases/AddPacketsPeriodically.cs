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
			Observable.Interval(TimeSpan.FromSeconds(.1f))
				.Subscribe(_ => AddPacket(nodeId, commandQueue));
		}

		private void AddPacket(NodeIdentifier nodeId, IEnqueueCommand<NetworkCommand> commandQueue)
		{
			commandQueue.Enqueue(new NetworkCommand.AddPacket(new PacketIdentifier(), RandomPacketType, nodeId));
		}

		private PacketType RandomPacketType
			=> (PacketType)UnityEngine.Random.Range(0, PacketTypes);
	}
}