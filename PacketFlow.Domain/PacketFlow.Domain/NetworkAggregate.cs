using Functional.Maybe;
using Workshop.Core;

namespace PacketFlow.Domain
{
	public class NetworkAggregate : AggregateRoot<NetworkEvent, NetworkState>, IHandleCommand<NetworkCommand, NetworkError>
	{
		public NetworkAggregate() : base(new NetworkState()) { }

		public Maybe<NetworkError> HandleCommand(NetworkCommand command)
			=> command.Match(
				AddGatewayNode,
				AddRouterNode,
				AddConsumerNode,
				LinkNodes,
				AddPacket,
				SetPacketTypeDirection
			);

		private Maybe<NetworkError> AddGatewayNode(NetworkCommand.AddGatewayNode command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.Record(() => new NetworkEvent.NodeAdded(new Node.Gateway(command.NodeId, command.Position, new NodeQueue(command.Capacity), new NodePortSet())))
				.Execute();

		private Maybe<NetworkError> AddRouterNode(NetworkCommand.AddRouterNode command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.Record(() => new NetworkEvent.NodeAdded(new Node.Router(command.NodeId, command.Position, new NodeQueue(command.Capacity), new NodePortSet(), new RouterState())))
				.Execute();

		private Maybe<NetworkError> AddConsumerNode(NetworkCommand.AddConsumerNode command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.Record(() => new NetworkEvent.NodeAdded(new Node.Consumer(command.NodeId, command.Position, new NodeQueue(command.Capacity), new NodePortSet())))
				.Execute();

		private Maybe<NetworkError> LinkNodes(NetworkCommand.LinkNodes command)
		{
			var newLinkId = new LinkIdentifier();

			return this.BuildCommand<NetworkEvent, NetworkError>()
				.FailIf(() => !State.Nodes.ContainsKey(command.Source), () => NetworkError.UnknownNode)
				.FailIf(() => !State.Nodes.ContainsKey(command.Sink), () => NetworkError.UnknownNode)
				.FailIf(() => !State.Nodes[command.Source].Ports.IsDisconnected(command.SourcePort), () => NetworkError.PortFull)
				.FailIf(() => !State.Nodes[command.Sink].Ports.IsDisconnected(command.SinkPort), () => NetworkError.PortFull)
				.Record(() => new NetworkEvent.LinkAdded(new Link(newLinkId, command.Source, command.Sink, command.Attributes)))
				.Record(() => new NetworkEvent.PortAssigned(command.Source, command.SourcePort, newLinkId, ConnectionDirection.Output))
				.Record(() => new NetworkEvent.PortAssigned(command.Sink, command.SinkPort, newLinkId, ConnectionDirection.Input))
				.Execute();
		}

		private Maybe<NetworkError> AddPacket(NetworkCommand.AddPacket command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.FailIf(() => State.Packets.ContainsKey(command.PackedId), () => NetworkError.PacketAlreadyAdded)
				.FailIf(() => !State.Nodes.ContainsKey(command.NodeId), () => NetworkError.UnknownNode)
				.FailIf(() => State.Nodes[command.NodeId].Queue.IsFull, () => NetworkError.QueueFull)
				.Record(() => new NetworkEvent.PacketAdded(new Packet(command.PackedId, command.Type)))
				.Record(() => new NetworkEvent.PacketEnqueued(command.PackedId, command.NodeId))
				.Execute();

		private Maybe<NetworkError> SetPacketTypeDirection(NetworkCommand.IncrementPacketTypeDirection command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.FailIf(() => !State.Nodes.ContainsKey(command.NodeId), () => NetworkError.UnknownNode)
				.FailIf(() => !(State.Nodes[command.NodeId] is Node.Router), () => NetworkError.NodeNotRouter)
				.Record(() => new NetworkEvent.PacketTypeDirectionChanged(command.NodeId, command.PacketType, (State.Nodes[command.NodeId] as Node.Router).NextOutputPort(command.PacketType)))
				.Execute();
	}
}
