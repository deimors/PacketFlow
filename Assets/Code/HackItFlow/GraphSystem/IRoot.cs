﻿using Assets.Code.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.HackItFlow.GraphSystem
{
	public interface IRoot
	{
		IReadOnlyReactiveSet<INode> Nodes { get; }

		IReadOnlyReactiveSet<ILink> Links { get; }
	}
}
