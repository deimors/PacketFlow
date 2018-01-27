﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Utilities
{
	public interface IReactiveSet<T> : IReadOnlyReactiveSet<T>
	{
		void Add(T value);

		void Remove(T value);
	}
}