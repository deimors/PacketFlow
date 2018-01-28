using Assets.Code.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.HackItFlow.CommandLineSystem
{
	public interface ICommandLineConsole
	{
		IReadOnlyReactiveCollection<string> Text { get; }

		void AddText(string firstLine, params string[] subsequentLines);
	}
}
