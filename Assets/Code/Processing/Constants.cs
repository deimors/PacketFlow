using PacketFlow.Domain;
using System;
using System.Collections.Generic;

namespace Assets.Code
{
	public static class Constants
	{
		public const int HACKER_PLAYER_MESSAGE_TYPE_ID = 880;
		public const int ADMIN_PLAYER_MESSAGE_TYPE_ID = 881;
		public const int HACKER_PLAYER_TYPE = 0;
		public const int ADMIN_PLAYER_TYPE = 1;
		public const int ADD_NODE_TRE_PAYLOAD_TYPE = 0;
		public const int EVENT_PAYLOAD_TYPE = 1;

		public static class TransportNodeType
		{
			public const int Gateway = 0;
			public const int Router = 1;
			public const int Consumer = 2;
		}		
	}
}
