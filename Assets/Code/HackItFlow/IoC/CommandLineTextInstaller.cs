using Assets.Code.HackItFlow.CommandLineSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Code.HackItFlow.IoC
{
	public class CommandLineTextInstaller : MonoInstaller
	{
		[Inject]
		public ICommandLineText Text { get; set; }

		public override void InstallBindings()
		{
			Container.BindInstance(Text);
		}
	}
}
