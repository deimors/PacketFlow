using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.HackItFlow.GraphSystem
{
	public interface INode
	{
		IReadOnlyReactiveProperty<float> X { get; }

		IReadOnlyReactiveProperty<float> Y { get; }
	}
}
