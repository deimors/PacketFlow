﻿using PacketFlow.Domain;
using System;
using UnityEngine;

namespace Assets.Code.Processing.TransportEvents.Mapping
{
	public static partial class NetworkEventAndPacketFlowMessageBidirectionalMapper
	{
		private const int NODE_ADDED_EVENT_TYPE_ID = 1;
		private const int LINK_ADDED_EVENT_TYPE_ID = 2;
		private const int PACKET_ADDED_EVENT_TYPE_ID = 3;
		private const int PACKET_ENQUEUED_EVENT_TYPE_ID = 4;
		private const int PORT_ASSIGNED_EVENT_TYPE_ID = 5;
		private const int PACKET_TYPE_DIRECTION_CHANGED = 6;

		public static PacketFlowMessage Map(NetworkEvent networkEvent)
		{
			return new PacketFlowMessage()
			{
				senderID = -99,
				senderType = -99,
				payloadType = networkEvent.Match(
					nodeAddedEvent => NODE_ADDED_EVENT_TYPE_ID,
					linkAddedEvent => LINK_ADDED_EVENT_TYPE_ID,
					packetAddedEvent => PACKET_ADDED_EVENT_TYPE_ID,
					packetEnqueuedEvent => PACKET_ENQUEUED_EVENT_TYPE_ID,
					portAssignedEvent => PORT_ASSIGNED_EVENT_TYPE_ID,
					packetTypeDirectionChanged => PACKET_TYPE_DIRECTION_CHANGED),
				payload = JsonUtility.ToJson(networkEvent.Match(
					nodeAddedEvent => nodeAddedEvent.ToNodeAddedEventTransport(),
					linkAddedEvent => linkAddedEvent.ToLinkAddedEventTransport(),
					packetAddedEvent => packetAddedEvent.ToPacketAddedEventTransport(),
					packetEnqueuedEvent => packetEnqueuedEvent.ToPacketEnqueuedEventTransport(),
					portAssignedEvent => portAssignedEvent.ToPortAssignedEventTransport(),
					packetTypeDirectionChangedEvent => packetTypeDirectionChangedEvent.ToPacketTypeDirectionChanged()))
			};
		}

		public static NetworkEvent Map(PacketFlowMessage message)
		{
			switch (message.payloadType)
			{
				case (NODE_ADDED_EVENT_TYPE_ID):
					return JsonUtility.FromJson<NodeAddedEventTransport>(message.payload).ToNetworkEvent();
				case (LINK_ADDED_EVENT_TYPE_ID):
					return JsonUtility.FromJson<LinkAddedEventTransport>(message.payload).ToNetworkEvent();
				case (PACKET_ADDED_EVENT_TYPE_ID):
					return JsonUtility.FromJson<PacketAddedEventTransport>(message.payload).ToNetworkEvent();
				case (PACKET_ENQUEUED_EVENT_TYPE_ID):
					return JsonUtility.FromJson<PacketEnqueuedEventTransport>(message.payload).ToNetworkEvent();
				case (PORT_ASSIGNED_EVENT_TYPE_ID):
					return JsonUtility.FromJson<PortAssignedEventTransport>(message.payload).ToNetworkEvent();
				case (PACKET_TYPE_DIRECTION_CHANGED):
					return JsonUtility.FromJson<PacketTypeDirectionChangedEventTransport>(message.payload).ToNetworkEvent();
				default:
					throw new NotImplementedException();
			}
		}
	}
}