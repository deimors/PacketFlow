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

		private void AddToTextCollection(string firstLine, params string[] subsequentLines)
		{
			_text.Add($"/usr/c00l > {firstLine}");

			foreach (var line in subsequentLines)
			{
				_text.Add(line);
			}
		}

		public void AddText(string firstLine, params string[] subsequentLines)
		{
			_nextText = _nextText.AddMilliseconds(UnityEngine.Random.Range(50, 200));

			var deltaTime = _nextText - DateTime.Now;

			if (deltaTime < TimeSpan.Zero)
			{
				AddToTextCollection(firstLine, subsequentLines);
				_nextText = DateTime.Now;
			}
			else
			{
				Observable.Timer(deltaTime).Subscribe(_ => AddToTextCollection(firstLine));
			}
		}
	}
}
