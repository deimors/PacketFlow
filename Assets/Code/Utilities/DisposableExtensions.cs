using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Utilities
{
	public static class DisposableExtensions
	{
		public static T DisposeWith<T>(this T value, ICanBeDisposed target)
			where T : IDisposable
		{
			target.Disposed += value.Dispose;

			return value;
		}
	}
}
