using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.Utilities
{
	public class ReactiveSet<T> : IReactiveSet<T>, IDisposable	
	{
		private readonly HashSet<T> _set = new HashSet<T>();

		private readonly Subject<T> _addSubject = new Subject<T>();

		private readonly Subject<T> _removeSubject = new Subject<T>();

		public void Add(T value)
		{
			if (_set.Add(value))
				_addSubject.OnNext(value);
		}

		public void Remove(T value)
		{
			if (_set.Remove(value))
				_removeSubject.OnNext(value);
		}

		public void Dispose()
		{
			_set.Clear();
			_addSubject.Dispose();
			_removeSubject.Dispose();
		}

		public IObservable<T> ObserveAdd()
			=> _addSubject;

		public IObservable<T> ObserveRemove()
			=> _removeSubject;
	}
}
