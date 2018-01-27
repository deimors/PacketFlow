using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Utilities
{
	public class DisposableMonoBehaviour : MonoBehaviour, ICanBeDisposed
	{
		private void OnDestroy()
		{
			Disposed?.Invoke();
			Disposed = null;
		}

		public event Action Disposed;
	}
}
