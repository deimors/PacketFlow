using Assets.Code.HackItFlow.CommandLineSystem;
using Assets.Code.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Code.HackItFlow.Presentation
{
	public class CommandLineConsoleBehaviour : DisposableMonoBehaviour
	{
		private ICommandLineConsole _commandLineConsole;

		[Inject]
		public void Initialize(ICommandLineConsole commandLineConsole)
		{
			_commandLineConsole = commandLineConsole;
		}

		[Inject]
		public ReactiveCollection<string> GetConsoleText()
		{
			return _commandLineConsole.Text.ToReactiveCollection();
		}
	}
}
