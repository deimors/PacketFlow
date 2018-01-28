using PacketFlow.Domain;
using PacketFlow.Presentation.Packet;
using PacketFlow.UseCases;
using UnityEngine;
using Zenject;

namespace PacketFlow.Presentation.Link
{
	public class LinkTransmissionPresenter : MonoBehaviour, IDisplayPacketTransmission
	{
		[SerializeField]
		private NetworkLink _link;

		[Inject]
		public PacketContainer.Factory PacketFactory { get; set; }

		public void Display(PacketIdentifier packetId, float time)
		{
			_link.AddGameObject(PacketFactory.Create(packetId).gameObject, time);
		}
	}
}
