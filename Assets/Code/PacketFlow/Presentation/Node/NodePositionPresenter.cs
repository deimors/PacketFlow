using PacketFlow.Domain;
using PacketFlow.UseCases;
using UnityEngine;

namespace PacketFlow.Presentation.Node
{
	public class NodePositionPresenter : MonoBehaviour, IDisplayNodePosition
	{
		public NodePosition Position
		{
			set
			{
				transform.position = new Vector3(value.X, value.Y);
			}
		}
	}
}
