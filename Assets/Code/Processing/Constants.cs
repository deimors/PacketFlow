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
		public const int COMMAND_PAYLOAD_TYPE = 0;
		public const int EVENT_PAYLOAD_TYPE = 1;

		public static class TransportNodeType
		{
			public const int Gateway = 0;
			public const int Router = 1;
			public const int Consumer = 2;
		}

		public static class TransportPortDirection
		{
			public const int Top = 0;
			public const int Right = 1;
			public const int Bottom = 2;
			public const int Left = 3;
		}

		public static class PacketType
		{
			public const int Red = 0;
			public const int Blue = 1;
			public const int Green = 2;
		}
	}
}
