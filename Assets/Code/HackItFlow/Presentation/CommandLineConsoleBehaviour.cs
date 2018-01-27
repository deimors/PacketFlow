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
		public void Initialize(ICommandLineConsole commandLineConsole)
		{
			_commandLineConsole = commandLineConsole;

			foreach (var line in _commandLineConsole.Text)
			{
				AddLine(line);
			}

			_commandLineConsole
				.Text
				.ObserveAdd()
				.Select(e => e.Value)
				.Subscribe(AddLine)
				.DisposeWith(this);
		}

		private void AddLine(string line)
		{
			if (_textMeshProText.text.Length > 0)
			{
				_textMeshProText.text += Environment.NewLine;
			}

			_textMeshProText.text += line;
		}
	}
}
