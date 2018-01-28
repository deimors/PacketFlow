﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.SLAMeter
{
	public class SLAMeter : ISLAMeter
	{
		public ReactiveProperty<int> SucceededCount { get; private set; }

		IReadOnlyReactiveProperty<int> ISLAMeter.SucceededCount => SucceededCount;

		public ReactiveProperty<int> FailedCount { get; private set; }

		IReadOnlyReactiveProperty<int> ISLAMeter.FailedCount => FailedCount;

		public float FailureThreashold { get; private set; }

		public int MinimumPacketsRequired { get; private set; }

		public void InitializeMeter(float threashold, int minimumNumberOfPackets)
		{
			FailureThreashold = threashold;
			MinimumPacketsRequired = minimumNumberOfPackets;
		}

		public bool TryAddPackets(int count, PacketStatus packetStatus)
		{
			switch (packetStatus)
			{
				case PacketStatus.Failure:
					FailedCount.Value += 1;
					break;
				case PacketStatus.Success:
					SucceededCount.Value += 1;
					break;
			}

			var failedCount = FailedCount.Value != 0 ? FailedCount.Value : 1.0f;

			if ((SucceededCount.Value / (float)(SucceededCount.Value + FailedCount.Value) <= FailureThreashold) 
				&& (FailedCount.Value + SucceededCount.Value >= MinimumPacketsRequired))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
