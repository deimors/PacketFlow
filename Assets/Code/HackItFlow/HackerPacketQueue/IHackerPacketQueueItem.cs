using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.HackItFlow.HackerPacketQueue
{
	public interface IHackerPacketQueueItem
	{
		HackerPacketQueueItemType Type { get; set; }

		HackerPacketQueueItemColour Colour { get; set; }
	}
}
