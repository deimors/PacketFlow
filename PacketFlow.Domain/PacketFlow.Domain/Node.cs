using Functional.Maybe;
using OneOf;
using System;
using System.Linq;
using Workshop.Core;

namespace PacketFlow.Domain
{
	public abstract class Node : OneOfBase<Node.Gateway, Node.Router, Node.Consumer>
	{
		public NodeIdentifier Id { get; }
		public NodePosition Position { get; }
		public NodeQueue Queue { get; }
		public NodePortSet Ports { get; }

		public Node(NodeIdentifier id, NodePosition position, NodeQueue queue, NodePortSet ports)
		{
			Id = id ?? throw new ArgumentNullException(nameof(id));
			Position = position ?? throw new ArgumentNullException(nameof(position));
			Queue = queue ?? throw new ArgumentNullException(nameof(queue));
			Ports = ports ?? throw new ArgumentNullException(nameof(ports));
		}

		public abstract Node With(
			Func<NodeQueue, NodeQueue> queue = null,
			Func<NodePortSet, NodePortSet> ports = null
		);

		public class Gateway : Node
		{
			public Gateway(NodeIdentifier id, NodePosition position, NodeQueue queue, NodePortSet ports) : base(id, position, queue, ports)
			{
			}

			public override Node With(
				Func<NodeQueue, NodeQueue> queue = null,
				Func<NodePortSet, NodePortSet> ports = null
			) => new Gateway(
				Id,
				Position,
				(queue ?? Function.Ident)(Queue),
				(ports ?? Function.Ident)(Ports)
			);
		}

		public class Router : Node
		{
			public Router(NodeIdentifier id, NodePosition position, NodeQueue queue, NodePortSet ports, RouterState state) : base(id, position, queue, ports)
			{
				State = state ?? throw new ArgumentNullException(nameof(state));
			}

			public RouterState State { get; }

			public override Node With(
				Func<NodeQueue, NodeQueue> queue = null,
				Func<NodePortSet, NodePortSet> ports = null
			) => new Router(
				Id,
				Position,
				(queue ?? Function.Ident)(Queue),
				(ports ?? Function.Ident)(Ports),
				State
			);

			public Node With(
				Func<RouterState, RouterState> state = null
			) => new Router(
				Id,
				Position,
				Queue,
				Ports,
				(state ?? Function.Ident)(State)
			);

			public PortDirection NextOutputPort(PacketType packetType)
			{
				var outputs = Ports.Outputs.ToArray();
				var currentIndex = outputs.Where(port => port == Ports[State[packetType]]).Select((port, i) => i).FirstMaybe();
				
				return currentIndex.SelectOrElse(
					index => outputs[index + 1 % outputs.Length].Port,
					() => PortDirection.Top
				);
			}
		}

		public class Consumer : Node
		{
			public Consumer(NodeIdentifier id, NodePosition position, NodeQueue queue, NodePortSet ports) : base(id, position, queue, ports)
			{
			}

			public override Node With(
				Func<NodeQueue, NodeQueue> queue = null,
				Func<NodePortSet, NodePortSet> ports = null
			) => new Consumer(
				Id,
				Position,
				(queue ?? Function.Ident)(Queue),
				(ports ?? Function.Ident)(Ports)
			);
		}
	}
}
