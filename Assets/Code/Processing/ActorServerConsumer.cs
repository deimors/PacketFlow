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
	public class ActorServerConsumer : MonoBehaviour, IActorServer<NetworkEvent, NetworkCommand>
	{
		private readonly ConcurrentQueue<PacketFlowMessage> _messageQueue = new ConcurrentQueue<PacketFlowMessage>();
		private readonly ClientEventDispatcher _eventDispatcher = new ClientEventDispatcher();

		public NetworkManager NetworkManagerInstance;			

		public void SendEvent(NetworkEvent @event)
		{
			var eventToSendAcrossWire = NetworkEventAndPacketFlowMessageBidirectionalMapper.Map(@event);
			NetworkServer.SendToAll(SERVER_MESSAGE_TYPE_ID, eventToSendAcrossWire);
		}


		void Update()
		{
			if (!NetworkManagerInstance.IsClientConnected())
				return;

			if (!NetworkManagerInstance.client.handlers.ContainsKey(HACKER_PLAYER_MESSAGE_TYPE_ID))
			{
				NetworkManagerInstance.client.RegisterHandler(HACKER_PLAYER_MESSAGE_TYPE_ID, networkMessage =>
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
					var command = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(message);
					CommandSubject.OnNext(command);
				}
			}
		}

		private int SenderID => NetworkManagerInstance?.client?.connection?.connectionId ?? 0;
		private static bool IsAServer => NetworkServer.connections.Count > 0; // server has connections, client does not (https://stackoverflow.com/a/41685717)

		private readonly ISubject<NetworkCommand> CommandSubject = new Subject<NetworkCommand>();
		public IObservable<NetworkCommand> ReceivedCommands => CommandSubject;
	}
}