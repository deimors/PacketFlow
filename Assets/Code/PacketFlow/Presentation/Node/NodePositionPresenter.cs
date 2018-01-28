using PacketFlow.Domain;
using PacketFlow.UseCases;
using UnityEngine;

namespace PacketFlow.Presentation.Node
{
	public class NodePositionPresenter : MonoBehaviour, IDisplayNodePosition
	{
		private readonly NodePositionToVector3Converter _positionConverter = new NodePositionToVector3Converter();
		public NodePosition Position
		{
			set
			{
				transform.localPosition = _positionConverter.Convert(value);
			}
		}
	}
}
