using PacketFlow.Domain;
using System;
using UniRx;
using UnityEngine;

namespace PacketFlow.UseCases
{

	public class SetNodePositionWhenNodeAdded
	{
		public SetNodePositionWhenNodeAdded(NodeIdentifier nodeId, IObservable<NetworkEvent> networkEvents, IDisplayNodePosition displayNodePosition)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.NodeAdded>()
				.Where(nodeAdded => nodeAdded.Node.Id == nodeId)
				.Subscribe(nodeAdded => displayNodePosition.Position = nodeAdded.Node.Position);
		}
	}
}