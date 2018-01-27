using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.HackItFlow.GraphSystem
{
	public class LoadBalancerNode : INode
	{
		public ReactiveProperty<float> X { get; } = new ReactiveProperty<float>();

		IReadOnlyReactiveProperty<float> INode.X => X;

		public ReactiveProperty<float> Y { get; } = new ReactiveProperty<float>();

		IReadOnlyReactiveProperty<float> INode.Y => Y;
	}
}
