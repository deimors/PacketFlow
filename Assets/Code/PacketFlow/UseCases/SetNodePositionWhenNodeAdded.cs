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
				.Do(nodeAdded => Debug.Log($"NodeAdded : {nodeAdded.Node.Id} ({nodeId})"))
				.Where(nodeAdded => nodeAdded.Node.Id == nodeId)
				.Do(nodeAdded => Debug.Log($"Setting position for {nodeAdded.Node.Id} to {nodeAdded.Node.Position}"))
				.Subscribe(nodeAdded => displayNodePosition.Position = nodeAdded.Node.Position);
		}
	}
}