using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.HackItFlow.CommandLineSystem
{
	public class CommandLineTextAddedEvent
	{
		public string Text { get; }

		public CommandLineTextAddedEvent(string text)
		{
			Text = text;
		}
	}
}
