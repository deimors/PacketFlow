using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.SLAMeter
{
	public class SLAMeter : ISLAMeter
	{
		public ReactiveProperty<int> CurrentPackets { get; set; } = new ReactiveProperty<int>();

		IReadOnlyReactiveProperty<int> ISLAMeter.CurrentPackets => CurrentPackets;

		private int _maxPackets;

		public int MaxPackets => _maxPackets;

		int ISLAMeter.MaxPackets => MaxPackets;

		public bool TryAddPackets(int level)
		{
			return true;
		}

		public void InitializeThreashold(int maxPackets)
		{
			_maxPackets = maxPackets;
		}
	}
}
