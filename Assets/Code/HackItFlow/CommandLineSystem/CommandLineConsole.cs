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

		private ReactiveCollection<string> _text;

		public IReadOnlyReactiveCollection<string> Text
		{
			get
			{
				return _text;
			}
		}

		IReadOnlyReactiveCollection<string> ICommandLineConsole.Text => Text;

		public void AddText(string text)
		{
			if (_text.Count >= MAX_LINES_OF_TEXT)
				_text.RemoveAt(0);

			_text.Add(text);
		}
	}
}
