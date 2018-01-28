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
		private const int MAX_QUEUE_SIZE = 9;	// Queue of 8 and 1 infected packet

		private ReactiveCollection<IHackerPacketQueueItem> _items = new ReactiveCollection<IHackerPacketQueueItem>();

		public IReadOnlyReactiveCollection<IHackerPacketQueueItem> Items => _items;

		public HackerPacketQueue()
		{
			Observable
				.Interval(TimeSpan.FromSeconds(1))
				.Subscribe(_ => UpdateQueue());

			UpdateQueue();
		}

		private void UpdateQueue()
		{
			if (_items.Count >= 8)
			{
				_items.RemoveAt(_items.Count - 1);

			}

			while (_items.Count < 8)
			{
				_items.Insert(0, new HackerPacketQueueItem()
				{
					Colour = (HackerPacketQueueItemColour)UnityEngine.Random.Range(0, 3),
					Type = HackerPacketQueueItemType.Type1
				});
			}
		}

		public bool TryAddItem(HackerPacketQueueItemType type, HackerPacketQueueItemColour colour)
		{
			if (_items.Count >= MAX_QUEUE_SIZE)
			{
				return false;
			}

			int halfSize = _items.Count / 2;

			_items.Insert(halfSize, new HackerPacketQueueItem() { Type = type, Colour = colour });

			return true;
		}
	}
}
