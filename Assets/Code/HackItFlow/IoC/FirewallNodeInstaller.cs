using Assets.Code.HackItFlow.GraphSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Code.HackItFlow.IoC
{
	public class FirewallNodeInstaller : MonoInstaller
	{
		[Inject]
		public INode Node { get; set; }

		public override void InstallBindings()
		{
			Container.BindInstance(Node);
		}
	}
}
