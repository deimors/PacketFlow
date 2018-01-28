using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.HackItFlow.HackerPacketQueue
{
	public class HackerPacketQueueItem : IHackerPacketQueueItem
	{
		public HackerPacketQueueItemType Type { get; set; }

		public HackerPacketQueueItemColour Colour { get; set; }
	}
}
