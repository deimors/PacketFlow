using Assets.Code;
using PacketFlow.Actors;
using System;
using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.Networking;
using static Assets.Code.Constants;

namespace Assets.Code.Processing
{
	public class ActorClientConsumer<TEvent, TCommand> : MonoBehaviour, IActorClient<TEvent, TCommand>
	{
		private readonly ConcurrentQueue<PacketFlowMessage> _messageQueue = new ConcurrentQueue<PacketFlowMessage>();
		private readonly ClientEventDispatcher _eventDispatcher = new ClientEventDispatcher();

		public NetworkManager NetworkManagerInstance;

		public IObservable<TEvent> ReceivedEvents;

		IObservable<TEvent> IActorClient<TEvent, TCommand>.ReceivedEvents
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void SendCommand(TCommand command)
		{
			if (!SafeToSend)
				return;

			var message = new CommandToPacketFlowMessageMapper<TCommand>().Map(SenderID, HACKER_PLAYER_TYPE, command);

			NetworkManagerInstance.client.Send(HACKER_PLAYER_MESSAGE_TYPE_ID, message);			
		}				

		void Update()
		{
			if (!NetworkManagerInstance.IsClientConnected())
				return;

			if (!NetworkManagerInstance.client.handlers.ContainsKey(ADMIN_PLAYER_MESSAGE_TYPE_ID))
			{
				NetworkManagerInstance.client.RegisterHandler(ADMIN_PLAYER_MESSAGE_TYPE_ID, networkMessage =>
				{
					var message = networkMessage.ReadMessage<PacketFlowMessage>();
					_messageQueue.Enqueue(message);
				});
			}

			while (!_messageQueue.IsEmpty)
			{
				PacketFlowMessage message;
				if (_messageQueue.TryDequeue(out message))
					_eventDispatcher.Dispatch(message);
			}
		}

		private bool SafeToSend => NetworkManagerInstance?.IsClientConnected() ?? false;
		private int SenderID => NetworkManagerInstance?.client?.connection?.connectionId ?? 0;
		private static bool IsAServer => NetworkServer.connections.Count > 0; // server has connections, client does not (https://stackoverflow.com/a/41685717)
	}
}
internal class ActorClientEventDispatcher
{
	public void Dispatch(PacketFlowMessage message)
	{
		Debug.Log("Client received message: " + message.payload);
	}
}