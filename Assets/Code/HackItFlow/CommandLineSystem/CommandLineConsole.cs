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

		private ReactiveCollection<string> _text = new ReactiveCollection<string>();

		public IReadOnlyReactiveCollection<string> Text => _text;

		private DateTime _nextText;

		public CommandLineConsole()
		{
			_text.Add("/usr/zeroc00l > ");
		}

		private void AddToTextCollection(string line)
		{
			_text[_text.Count - 1] += line;
			_text.Add("");
		}

		public void AddText(string firstLine, params string[] subsequentLines)
		{
			AddText(firstLine);

			foreach (var line in subsequentLines ?? Enumerable.Empty<string>())
				AddText(line);

			AddText("/usr/zeroc00l > ");
		}

		private void AddText(string line)
		{
			_nextText = _nextText.AddMilliseconds(UnityEngine.Random.Range(30, 200));

			var deltaTime = _nextText - DateTime.Now;

			if (deltaTime < TimeSpan.Zero)
			{
				AddToTextCollection(line);
				_nextText = DateTime.Now;
			}
			else
			{
				Observable.Timer(deltaTime).Subscribe(_ => AddToTextCollection(line));
			}
		}
	}
}
