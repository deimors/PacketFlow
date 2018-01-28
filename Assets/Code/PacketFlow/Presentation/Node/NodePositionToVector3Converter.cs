using PacketFlow.Domain;
using UnityEngine;

namespace PacketFlow.Presentation.Node
{
	public class NodePositionToVector3Converter
	{
		public Vector3 Convert(NodePosition position)
			=> new Vector3(position.X, position.Y);
	}
}
