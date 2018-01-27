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

		private CommandLineTextFactory _commandLineTextFactory;

		[Inject]
		public void Initialize(ICommandLineConsole commandLineConsole, CommandLineTextFactory commandLineTextFactory)
		{
			_commandLineConsole = commandLineConsole;
			_commandLineTextFactory = commandLineTextFactory;

			_commandLineConsole
				.LinesToDisplay
				.ObserveAdd()
				.OfType<ICommandLineText, ICommandLineText>()
				.Subscribe(AddText)
				.DisposeWith(this);

			_commandLineConsole
				.LinesToDisplay
				.ObserveRemove()
				.OfType<ICommandLineText, ICommandLineText>()
				.Subscribe(RemoveText)
				.DisposeWith(this);
		}

		private void AddText(ICommandLineText text) => _commandLineTextFactory.Create(text);

		private void RemoveText(ICommandLineText text)
		{
		}
	}
}
