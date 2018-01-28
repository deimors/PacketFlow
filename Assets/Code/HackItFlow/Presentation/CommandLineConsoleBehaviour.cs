using Assets.Code.HackItFlow.CommandLineSystem;
using Assets.Code.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;


namespace Assets.Code.HackItFlow.Presentation
{
	public class CommandLineConsoleBehaviour : DisposableMonoBehaviour
	{
		private ICommandLineConsole _commandLineConsole;

		[SerializeField]
		private TextMeshProUGUI _textMeshProText;

		private bool? _cursorVisible;
		public bool? CursorVisible
		{
			get { return _cursorVisible; }
			set
			{
				var oldValue = _cursorVisible ?? false;
				var newValue = value ?? false;

				_cursorVisible = value;

				if (oldValue && !newValue)
					_textMeshProText.text = _textMeshProText.text.Substring(0, _textMeshProText.text.Length - 1);
				else if (!oldValue && newValue)
					_textMeshProText.text += "_";

				
			}
		}

		[Inject]
		public void Initialize(ICommandLineConsole commandLineConsole, IObservable<CommandLineTextAddedEvent> textAdded)
		{
			_commandLineConsole = commandLineConsole;

			// Seed initial ASCII art
			_commandLineConsole.AddText(
				"initialize",
				"",
				"   ROFL:ROFL:ROFL:ROFL",
				"         ___^___ _",
				" L    __/      [] \\  ",
				"LOL===__           \\ ",
				" L      \\___ ___ ___]",
				"              I   I",
				"          ----------/",
				"");

			textAdded
				.Select(e => e.Text)
				.Subscribe(AppendText)
				.DisposeWith(this);

			Observable
				.Interval(TimeSpan.FromMilliseconds(500))
				.Subscribe(_ => ToggleCursor())
				.DisposeWith(this);
		}

		private void AppendText(string text)
		{
			CursorVisible = null;

			_textMeshProText.text += text;
		}

		private void ToggleCursor()
		{
			if (CursorVisible == null)
				CursorVisible = false;
			else
				CursorVisible = !CursorVisible;
		}
	}
}
