using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.HackItFlow.HackerPacketQueue
{
	public interface IHackerPacketQueue
	{
		IReadOnlyReactiveCollection<IHackerPacketQueueItem> Items { get; }

		bool TryAddItem(HackerPacketQueueItemType type, HackerPacketQueueItemColour colour);
	}
}
