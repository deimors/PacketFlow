using Functional.Maybe;
using PacketFlow.Domain;
using System;
using System.Collections.Generic;
using UniRx;

namespace PacketFlow.UseCases
{
	public class NodePositionReadModel
	{
		private readonly Dictionary<NodeIdentifier, NodePosition> _positions = new Dictionary<NodeIdentifier, NodePosition>();

		public NodePositionReadModel(IObservable<NetworkEvent> networkEvents)
		{
			networkEvents
				.OfType<NetworkEvent, NetworkEvent.NodeAdded>()
				.Subscribe(nodeAdded => _positions[nodeAdded.Node.Id] = nodeAdded.Node.Position);
		}

		public NodePosition this[NodeIdentifier nodeId]
			=> _positions.Lookup(nodeId).OrElseDefault();
	}
}