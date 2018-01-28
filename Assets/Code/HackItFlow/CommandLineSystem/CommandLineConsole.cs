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

		private IObserver<CommandLineTextAddedEvent> _textObserver;

		private DateTime _nextText;

		public CommandLineConsole(IObserver<CommandLineTextAddedEvent> textObserver)
		{
			_textObserver = textObserver;

			SendText("/usr/zeroc00l > ");
		}

		public void AddText(string firstLine, params string[] subsequentLines)
		{
			AddText(firstLine + Environment.NewLine);

			foreach (var line in subsequentLines ?? Enumerable.Empty<string>())
				AddText(line + Environment.NewLine);

			AddText("/usr/zeroc00l > ");
		}

		private void AddText(string line)
		{
			_nextText = _nextText.AddMilliseconds(UnityEngine.Random.Range(50, 200));

			var deltaTime = _nextText - DateTime.Now;

			if (deltaTime < TimeSpan.Zero)
			{
				SendText(line);
				_nextText = DateTime.Now;
			}
			else
			{
				Observable.Timer(deltaTime).Subscribe(_ => SendText(line));
			}
		}

		private void SendText(string text)
			=> _textObserver.OnNext(new CommandLineTextAddedEvent(text));
	}
}
