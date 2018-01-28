using PacketFlow.UseCases;
using UnityEngine;

namespace PacketFlow.Presentation.Packet
{
	public class PacketColourPresenter : MonoBehaviour, IDisplayPacketColour
	{
		[SerializeField]
		private SpriteRenderer _packetSprite;

		public void Display(Color color)
		{
			_packetSprite.color = color;
		}
	}
}
