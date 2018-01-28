using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.SLAMeter
{
	public interface ISLAMeter
	{
		IReadOnlyReactiveProperty<int> SucceededCount { get; }

		IReadOnlyReactiveProperty<int> FailedCount { get; }

		float FailureThreashold { get; }

		int MinimumPacketsRequired { get; }

		bool TryAddPackets(int count, PacketStatus packetStatus);

		void InitializeMeter(float threashold, int minimumNumberOfPackets);
	}
}
