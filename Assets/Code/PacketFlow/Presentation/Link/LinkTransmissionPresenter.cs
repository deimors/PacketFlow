using PacketFlow.Domain;
using PacketFlow.UseCases;
using UnityEngine;

namespace PacketFlow.Presentation.Link
{
	public class LinkTransmissionPresenter : MonoBehaviour, IDisplayPacketTransmission
	{
		[SerializeField]
		private NetworkLink _link;

		[SerializeField]
		private GameObject _prefab;

		public void Display(PacketType type, float time)
		{
			_link.AddGameObject(Instantiate(_prefab), time);
		}
	}
}
