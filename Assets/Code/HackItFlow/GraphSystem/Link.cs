using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.HackItFlow.GraphSystem
{
	public class Link : ILink
	{
		public INode NodeOne { get; }

		public INode NodeTwo { get; }

		public Link(INode nodeOne, INode nodeTwo)
		{
			NodeOne = nodeOne;
			NodeTwo = nodeTwo;
		}
	}
}
