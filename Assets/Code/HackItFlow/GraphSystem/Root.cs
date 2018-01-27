using Assets.Code.Utilities;
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
		public ReactiveSet<INode> Nodes = new ReactiveSet<INode>();

		IReadOnlyReactiveSet<INode> IRoot.Nodes => Nodes;

		public ReactiveSet<ILink> Links { get; } = new ReactiveSet<ILink>();

		IReadOnlyReactiveSet<ILink> IRoot.Links => Links;
	}
}
