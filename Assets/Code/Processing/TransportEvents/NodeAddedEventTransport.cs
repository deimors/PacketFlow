using System;
using System.Collections.Generic;

namespace Assets.Code.Processing.TransportEvents
{
	public class NodeAddedEventTransport
	{
		public SerializableGuid NodeID;
		public int NodeType;
		public float PositionX;
		public float PositionY;
		public List<SerializableGuid> QueueContent;
		public int QueueCapacity;
		public SerializableGuid TopPortLinkIdentifier;
		public int TopPortConnectionDirection;
		public SerializableGuid BottomPortLinkIdentifier;
		public int BottomPortConnectionDirection;
		public SerializableGuid LeftPortLinkIdentifier;
		public int LeftPortConnectionDirection;
		public SerializableGuid RightPortLinkIdentifier;
		public int RightPortConnectionDirection;
		public int TopPortDirection;
		public int BottomPortDirection;
		public int LeftPortDirection;
		public int RightPortDirection;
		public int[] RouterState;
	}
}