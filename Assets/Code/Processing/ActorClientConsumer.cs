using Assets.Code;
using Assets.Code.Processing.TransportEvents.Mapping;
using PacketFlow.Actors;
using PacketFlow.Domain;
using System;
using System.Collections.Concurrent;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using static Assets.Code.Constants;

namespace Assets.Code.Processing
{
	public class ActorClientConsumer : MonoBehaviour, IActorClient<NetworkEvent, NetworkCommand>
	{
		private readonly ConcurrentQueue<PacketFlowMessage> _messageQueue = new ConcurrentQueue<PacketFlowMessage>();

		public NetworkManager NetworkManagerInstance;

		private ISubject<NetworkEvent> EventSubject = new Subject<NetworkEvent>();
		public IObservable<NetworkEvent> ReceivedEvents => EventSubject;

		#region LOCAL FUNCTIONS ARE NOT ALLOWED 
		private NodePosition Position => new NodePosition(0.0f, 0.0f);
		private NodeIdentifier ID => new NodeIdentifier();
		private int Capacity => 10;
		private PortDirection Direction => PortDirection.Top;
		private PacketType PT => PacketType.Blue;
		#endregion

		public void SendCommand(NetworkCommand command)
		{
			var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(SenderID, HACKER_PLAYER_TYPE, command);
			NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);
		}				

		
		void Update()
		{
			if (!NetworkManagerInstance.IsClientConnected())
				return;

			if (!NetworkManagerInstance.client.handlers.ContainsKey(SERVER_MESSAGE_TYPE_ID))
			{
				NetworkManagerInstance.client.RegisterHandler(SERVER_MESSAGE_TYPE_ID, networkMessage =>
				{
					var message = networkMessage.ReadMessage<PacketFlowMessage>();
					_messageQueue.Enqueue(message);
				});
			}

			while (!_messageQueue.IsEmpty)
			{
				PacketFlowMessage message;
				if (_messageQueue.TryDequeue(out message))
				{
					var @event = NetworkEventAndPacketFlowMessageBidirectionalMapper.Map(message);
					EventSubject.OnNext(@event);
					Debug.Log("Message received: " + message.payload);
				}
			}
		}
		

		private bool SafeToSend => NetworkManagerInstance?.IsClientConnected() ?? false;
		private int SenderID => NetworkManagerInstance?.client?.connection?.connectionId ?? 0;
		private static bool IsAServer => NetworkServer.connections.Count > 0; // server has connections, client does not (https://stackoverflow.com/a/41685717)
	}
}