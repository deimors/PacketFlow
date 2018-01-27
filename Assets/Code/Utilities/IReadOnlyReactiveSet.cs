using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Utilities
{
	public interface IReadOnlyReactiveSet<T>
	{
		IObservable<T> ObserveAdd();

		IObservable<T> ObserveRemove();
	}
}
