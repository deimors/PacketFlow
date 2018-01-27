using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.HackItFlow.CommandLineSystem
{
	public class Command : ICommand
	{
		public ReactiveProperty<ActionType> Action { get; } = new ReactiveProperty<ActionType>();

		IReadOnlyReactiveProperty<ActionType> ICommand.Action => Action;
	}
}
