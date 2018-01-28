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
				.Subscribe(text => _textMeshProText.text += text)
				.DisposeWith(this);
		}
	}
}
