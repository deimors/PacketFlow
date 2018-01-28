using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Processing
{
	public class PresentationCommand
	{
		private readonly string _message;

		public PresentationCommand(string message)
		{
			_message = message;
		}

		public override string ToString() => _message;
	}
}
