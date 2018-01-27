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
		private const int MAX_LINES_OF_TEXT = 20;

		private ReactiveCollection<string> _text = new ReactiveCollection<string>()
		{
			"Test1",
			"test2",
			"test3"
		};

		public IReadOnlyReactiveCollection<string> Text => _text;

		public void AddText(string text)
		{
			_text.Add(text);
		}
	}
}
