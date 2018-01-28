using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;

namespace Assets.Code.HackItFlow.HackerPacketQueue
{
	public class HackerPacketQueue : IHackerPacketQueue
	{
		private ReactiveCollection<IHackerPacketQueueItem> _items = new ReactiveCollection<IHackerPacketQueueItem>();

		public IReadOnlyReactiveCollection<IHackerPacketQueueItem> Items => _items;

		public bool TryAddItem(HackerPacketQueueItemType type, HackerPacketQueueItemColour colour)
		{
			return true;
		}
	}
}
