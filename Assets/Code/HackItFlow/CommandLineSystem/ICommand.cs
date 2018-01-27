using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.HackItFlow.CommandLineSystem
{
	public interface ICommand
	{
		IReadOnlyReactiveProperty<ActionType> Action { get; }
	}
}
