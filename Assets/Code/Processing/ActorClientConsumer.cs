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
	public class PresentationCommand
	{
		private readonly string _message;

		public PresentationCommand(string message)
		{
			_message = message;
		}

		public override string ToString() => _message;
	}
	public class ActorClientConsumer : MonoBehaviour, IActorClient<NetworkEvent, PresentationCommand>
	{
		private readonly ConcurrentQueue<PacketFlowMessage> _messageQueue = new ConcurrentQueue<PacketFlowMessage>();
		private int _senderID => NetworkManagerInstance?.client?.connection?.connectionId ?? 0;

		public NetworkManager NetworkManagerInstance;

		private ISubject<NetworkEvent> _eventSubject = new Subject<NetworkEvent>();
		public IObservable<NetworkEvent> ReceivedEvents => _eventSubject;

		public void SendCommand(PresentationCommand command)
		{
			Debug.Log(command);
			/*var message = new NetworkCommandAndPacketFlowMessageBidirectionalMapper().Map(_senderID, HACKER_PLAYER_TYPE, command);
			NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);*/
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
					_eventSubject.OnNext(@event);
					Debug.Log("Message received: " + message.payload);
				}
			}
		}		
	}
}