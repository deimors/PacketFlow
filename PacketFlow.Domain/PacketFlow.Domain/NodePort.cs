using OneOf;
using System;

namespace PacketFlow.Domain
{
	public abstract class NodePort : OneOfBase<NodePort.Disconnected, NodePort.Connected>
	{
		protected NodePort(PortDirection port)
		{
			Port = port;
		}

		public PortDirection Port { get; }

		public class Disconnected : NodePort
		{
			public Disconnected(PortDirection direction) : base(direction)
			{
			}
		}

		public class Connected : NodePort
		{
			public Connected(PortDirection port, LinkIdentifier linkId, ConnectionDirection direction) : base(port)
			{
				LinkId = linkId ?? throw new ArgumentNullException(nameof(linkId));
				Direction = direction;
			}

			public LinkIdentifier LinkId { get; }
			public ConnectionDirection Direction { get; }
		}
	}
}
