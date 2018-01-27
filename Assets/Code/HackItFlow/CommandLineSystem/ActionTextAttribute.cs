using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.HackItFlow.CommandLineSystem
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class ActionTextAttribute : Attribute
	{
		public ActionTextAttribute(string commandText)
		{
			this.commandText = commandText;
		}

		private string commandText;

		public virtual string CommandText
		{
			get
			{
				return commandText;
			}
		}
	}
}
