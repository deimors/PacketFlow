using PacketFlow.Domain;
using PacketFlow.Presentation.Node;
using UnityEngine;
using Zenject;

namespace PacketFlow.Presentation
{
	public class PacketFlowInstaller : MonoInstaller
	{
		[SerializeField]
		private readonly GameObject _nodePrefab;

		public override void InstallBindings()
		{
			Container.BindFactory<NodeIdentifier, NodeContainer, NodeContainer.Factory>()
				.FromSubContainerResolve()
				.ByNewPrefab<NodeContainer>(_nodePrefab);
		}
	}
}