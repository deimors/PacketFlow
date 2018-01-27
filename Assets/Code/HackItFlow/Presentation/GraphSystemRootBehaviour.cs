using Assets.Code.HackItFlow.GraphSystem;
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
	public class GraphSystemRootBehaviour : DisposableMonoBehaviour
	{
		private IRoot _graphSystemRoot;

		private FirewallNodeFactory _firewallNodeFactory;

		[Inject]
		public void Initialize(IRoot graphSystemRoot, FirewallNodeFactory firewallNodeFactory)
		{
			_graphSystemRoot = graphSystemRoot;

			_graphSystemRoot
				.Nodes
				.ObserveAdd()
				.OfType<INode, IFirewallNode>()
				.Subscribe(AddFirewallNode)
				.DisposeWith(this);

			_graphSystemRoot
				.Nodes
				.ObserveRemove()
				.OfType<INode, IFirewallNode>()
				.Subscribe(RemoveFirewallNode)
				.DisposeWith(this);
		}

		private void AddFirewallNode(IFirewallNode node)
			=> _firewallNodeFactory.Create(node);

		private void RemoveFirewallNode(IFirewallNode node)
		{
		}
	}
}
