using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.HackItFlow.GraphSystem
{
	public interface ILink
	{
		INode NodeOne { get; }

		INode NodeTwo { get; }
	}
}
