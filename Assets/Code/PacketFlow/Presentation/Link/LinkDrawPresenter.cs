using PacketFlow.Domain;
using PacketFlow.Presentation.Node;
using PacketFlow.UseCases;
using UnityEngine;

namespace PacketFlow.Presentation.Link
{
	public class LinkDrawPresenter : MonoBehaviour, IDisplayLink
	{
		[SerializeField]
		private NetworkLink _link;

		private readonly NodePositionToVector3Converter _positionConverter = new NodePositionToVector3Converter();

		public void DisplayLink(NodePosition sourcePosition, NodePosition sinkPosition)
		{
			_link.StartPoint = _positionConverter.Convert(sourcePosition);
			_link.EndPoint = _positionConverter.Convert(sinkPosition);
		}
	}
}
