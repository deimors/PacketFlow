using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Assets.Code.HackItFlow.CommandLineSystem
{
	public enum ActionType
	{
		[ActionText("crack-password")]
		CrackPassword,

		[ActionText("infect-node")]
		InfectNode,

		[ActionText("connect-node")]
		ConnectNode,

		[ActionText("disconnect-node")]
		DisconnectNode
	}
}
