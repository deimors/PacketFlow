using Assets.Code.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.HackItFlow.CommandLineSystem
{
	public class CommandLineConsole : ICommandLineConsole
	{
		public ReactiveSet<ICommandLineText> LinesToDisplay { get; } = new ReactiveSet<ICommandLineText>();

		IReadOnlyReactiveSet<ICommandLineText> ICommandLineConsole.LinesToDisplay => LinesToDisplay;
	}
}
