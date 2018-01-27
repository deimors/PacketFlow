using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketFlow.Domain
{
	public class NodeIdentifier
	{

	}

	public class Node
	{
		public NodeIdentifier Id { get; }

		public Node(NodeIdentifier id)
		{
			Id = id ?? throw new ArgumentNullException(nameof(id));
		}
	}
}
