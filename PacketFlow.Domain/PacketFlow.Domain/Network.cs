using Functional.Maybe;
using OneOf;
using System.Collections.Generic;
using Workshop.Core;

namespace PacketFlow.Domain
{
	public abstract class NetworkEvent : OneOfBase<NetworkEvent.NodeAdded>
	{
		public class NodeAdded : NetworkEvent
		{
			public NodeAdded(Node node)
			{
				Node = node ?? throw new System.ArgumentNullException(nameof(node));
			}

			public Node Node { get; }
		}
	}

	public abstract class NetworkCommand : OneOfBase<NetworkCommand.AddNode>
	{
		public class AddNode : NetworkCommand
		{
			public AddNode(Node node)
			{
				Node = node ?? throw new System.ArgumentNullException(nameof(node));
			}

			public Node Node { get; }
		}
	}

	public enum NetworkError
	{

	}

	public class NetworkState : IApplyEvent<NetworkEvent>
	{
		private readonly Dictionary<NodeIdentifier, Node> _nodes = new Dictionary<NodeIdentifier, Node>();
		private readonly Dictionary<LinkIdentifier, Link> _links = new Dictionary<LinkIdentifier, Link>();

		public void ApplyEvent(NetworkEvent @event)
			=> @event.Switch(
				nodeAdded => _nodes.Add(nodeAdded.Node.Id, nodeAdded.Node)
			);
	}

	public class Network : AggregateRoot<NetworkEvent, NetworkState>, IHandleCommand<NetworkCommand, NetworkError>
	{
		public Network() : base(new NetworkState()) { }

		public Maybe<NetworkError> HandleCommand(NetworkCommand command)
			=> command.Match(
				AddNode
			);

		private Maybe<NetworkError> AddNode(NetworkCommand.AddNode command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.Execute();
	}
}
