using Functional.Maybe;
using OneOf;
using System.Collections.Generic;
using Workshop.Core;

namespace PacketFlow.Domain
{
	public abstract class NetworkEvent : OneOfBase<NetworkEvent.NodeAdded, NetworkEvent.LinkAdded>
	{
		public class NodeAdded : NetworkEvent
		{
			public NodeAdded(Node node)
			{
				Node = node ?? throw new System.ArgumentNullException(nameof(node));
			}

			public Node Node { get; }
		}

		public class LinkAdded : NetworkEvent
		{
			public LinkAdded(Link link)
			{
				Link = link ?? throw new System.ArgumentNullException(nameof(link));
			}

			public Link Link { get; }
		}
	}

	public abstract class NetworkCommand : OneOfBase<NetworkCommand.AddNode, NetworkCommand.LinkNodes>
	{
		public class AddNode : NetworkCommand
		{
			public AddNode(Node node)
			{
				Node = node ?? throw new System.ArgumentNullException(nameof(node));
			}

			public Node Node { get; }
		}

		public class LinkNodes : NetworkCommand
		{
			public LinkNodes(NodeIdentifier source, NodeIdentifier sink)
			{
				Source = source ?? throw new System.ArgumentNullException(nameof(source));
				Sink = sink ?? throw new System.ArgumentNullException(nameof(sink));
			}

			public NodeIdentifier Source { get; }
			public NodeIdentifier Sink { get; }
		}
	}

	public enum NetworkError
	{
		UnknownNode
	}

	public class NetworkState : IApplyEvent<NetworkEvent>
	{
		private readonly Dictionary<NodeIdentifier, Node> _nodes = new Dictionary<NodeIdentifier, Node>();
		private readonly Dictionary<LinkIdentifier, Link> _links = new Dictionary<LinkIdentifier, Link>();

		public IReadOnlyDictionary<NodeIdentifier, Node> Nodes { get; }
		public IReadOnlyDictionary<LinkIdentifier, Link> Links { get; }

		public void ApplyEvent(NetworkEvent @event)
			=> @event.Switch(
				nodeAdded => _nodes.Add(nodeAdded.Node.Id, nodeAdded.Node),
				linkAdded => _links.Add(linkAdded.Link.Id, linkAdded.Link)
			);
	}

	public class NetworkAggregate : AggregateRoot<NetworkEvent, NetworkState>, IHandleCommand<NetworkCommand, NetworkError>
	{
		public NetworkAggregate() : base(new NetworkState()) { }

		public Maybe<NetworkError> HandleCommand(NetworkCommand command)
			=> command.Match(
				AddNode,
				LinkNodes
			);

		private Maybe<NetworkError> AddNode(NetworkCommand.AddNode command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.Execute();

		private Maybe<NetworkError> LinkNodes(NetworkCommand.LinkNodes command)
			=> this.BuildCommand<NetworkEvent, NetworkError>()
				.FailIf(() => !State.Nodes.ContainsKey(command.Source), () => NetworkError.UnknownNode)
				.FailIf(() => !State.Nodes.ContainsKey(command.Sink), () => NetworkError.UnknownNode)
				.Record(() => new NetworkEvent.LinkAdded(new Link(new LinkIdentifier(), command.Source, command.Sink)))
				.Execute();
	}
}
