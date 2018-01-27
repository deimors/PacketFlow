using Assets.Code.HackItFlow.CommandLineSystem;
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
	public class CommandLineInstaller : MonoInstaller
	{
		[SerializeField]
		private GameObject _commandLinePrefab;

		[SerializeField]
		private GameObject _commandLineObject;

		public override void InstallBindings()
		{
			//Container.BindInterfacesAndSelfTo<Root>().AsSingle();

			//Container
			//	.BindFactory<IFirewallNode, IFirewallNode, FirewallNodeFactory>()
			//	.FromSubContainerResolve()
			//	.ByNewPrefab<FirewallNodeInstaller>(_firewallNodePrefab)
			//	.UnderTransform(_graphSystemObject.transform);

			Container.BindInterfacesAndSelfTo<CommandLineConsole>().AsSingle();

			Container
				.BindFactory<ICommandLineText, ICommandLineText, CommandLineTextFactory>()
				.FromSubContainerResolve()
				.ByNewPrefab<CommandLineTextInstaller>(_commandLinePrefab)
				.UnderTransform(_commandLineObject.transform);
		}
	}
}
