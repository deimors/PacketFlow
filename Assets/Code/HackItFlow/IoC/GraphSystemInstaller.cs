using Assets.Code.HackItFlow.GraphSystem;
using Assets.Code.HackItFlow.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Code.HackItFlow.IoC
{
	public class GraphSystemInstaller : MonoInstaller
	{
		[SerializeField]
		private GameObject _firewallNodePrefab;

		[SerializeField]
		private GameObject _graphSystemObject;

		public override void InstallBindings()
		{
			Container.Bind<IRoot>().To<Root>().AsSingle();
			
			Container
				.BindFactory<IFirewallNode, GameObject, FirewallNodeFactory>()
				.FromSubContainerResolve()
				.ByNewPrefab<FirewallNodeInstaller>(_firewallNodePrefab);
		}
	}
}
