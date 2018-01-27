using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.HackItFlow.GraphSystem
{
	public class Root : IRoot
	{
		public ReactiveCollection<INode> Nodes = new ReactiveCollection<INode>();

		IReadOnlyReactiveCollection<INode> IRoot.Nodes => Nodes;

		public ReactiveCollection<ILink> Links { get; } = new ReactiveCollection<ILink>();

		IReadOnlyReactiveCollection<ILink> IRoot.Links => Links;
	}
}
