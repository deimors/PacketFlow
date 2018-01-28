using Functional.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PacketFlow.Domain
{
	public class NodePortSet
	{
		private readonly NodePort[] _ports; 

		public NodePortSet()
		{
			_ports = Enum.GetValues(typeof(PortDirection)).OfType<PortDirection>().Select(port => new NodePort.Disconnected(port)).Cast<NodePort>().ToArray();
		}

		public NodePortSet(NodePort[] ports)
		{
			_ports = ports;
		}

		public NodePort this[PortDirection direction]
		{
			get
			{
				return _ports[(int)direction];
			}
		}

		public IEnumerable<NodePort> Inputs
			=> _ports.Where(port => port.Match(_ => false, connected => connected.Direction == ConnectionDirection.Input));

		public IEnumerable<NodePort> Outputs
			=> _ports.Where(port => port.Match(_ => false, connected => connected.Direction == ConnectionDirection.Output));

		public bool IsDisconnected(PortDirection direction)
			=> _ports[(int)direction] is NodePort.Disconnected;

		public NodePortSet ConnectPort(PortDirection port, LinkIdentifier linkId, ConnectionDirection direction)
			=> new NodePortSet(
				_ports
					.Select((p, i) => i == (int)port ? new NodePort.Connected(port, linkId, direction) : p)
					.ToArray()
			);

	}
}
