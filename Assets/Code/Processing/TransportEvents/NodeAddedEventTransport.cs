using System;

namespace Assets.Code.Processing.TransportEvents
{
	public class NodeAddedEventTransport
	{
		public Guid NodeID;
		public int NodeType;
		public float PositionX;
		public float PositionY;
		public Guid[] QueueContent;
		public int QueueCapacity;
		public Guid TopPortLinkIdentifier;
		public int TopPortConnectionDirection;
		public Guid BottomPortLinkIdentifier;
		public int BottomPortConnectionDirection;
		public Guid LeftPortLinkIdentifier;
		public int LeftPortConnectionDirection;
		public Guid RightPortLinkIdentifier;
		public int RightPortConnectionDirection;
	}
}